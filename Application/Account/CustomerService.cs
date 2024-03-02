using Application.Account.Dto;
using Application.Account.Interfaces;
using Application.Authentication;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;
using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Application.Account;
public class CustomerService : ServiceBase<CustomerDto, Customer>, IService<CustomerDto>, ICustomerService
{
    private readonly ICrypto _crypto = Crypto.GetInstance;
    private readonly SigningConfigurations _singingConfiguration;
    private readonly TokenConfiguration _tokenConfiguration;
    private IRepository<Flat> _flatRepository;

    public CustomerService(IMapper mapper, IRepository<Customer> customerRepository, IRepository<Flat> flatRepository, SigningConfigurations singingConfiguration, TokenConfiguration tokenConfiguration) : base(mapper, customerRepository)
    {
        _flatRepository = flatRepository;
        _singingConfiguration = singingConfiguration;
        _tokenConfiguration = tokenConfiguration;
    }
    public override CustomerDto Create(CustomerDto dto)
    {
        if (this.Repository.Exists(x => x.Login != null && x.Login.Email == dto.Email))
            throw new Exception("Usuário já existente na base.");


        Flat flat = this._flatRepository.GetById(dto.FlatId);

        if (flat == null)
            throw new Exception("Plano não existente ou não encontrado.");

        Card card = this.Mapper.Map<Card>(dto.Card);

        Customer customer = new() {  
            Id = Guid.NewGuid(),
            Name = dto.Name, 
            CPF = dto.CPF,
            Birth = dto.Birth,
            Phone = dto.Phone,
            Login = new() 
            {
                Email = dto.Email ?? "",
                Password = dto.Password ?? ""
            }
        };
        
        Address address = this.Mapper.Map<Address>(dto.Address);
        
        customer.CreateAccount(customer, address, flat, card);
        
        this.Repository.Save(customer);
        var result = this.Mapper.Map<CustomerDto>(customer);

        return result;
    }
    public override CustomerDto FindById(Guid id)
    {
        var customer = this.Repository.GetById(id);
        var result = this.Mapper.Map<CustomerDto>(customer);
        return result;
    }

    public override List<CustomerDto> FindAll(Guid userId)
    {
        var customers = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
        var result = this.Mapper.Map<List<CustomerDto>>(customers);
        return result;
    }
    public override CustomerDto Update(CustomerDto dto)
    {
        var customer = this.Mapper.Map<Customer>(dto);
        this.Repository.Update(customer);
        return this.Mapper.Map<CustomerDto>(customer);
    }
    public override bool Delete(CustomerDto dto)
    {
        var customer = this.Mapper.Map<Customer>(dto);
        this.Repository.Delete(customer);
        return true; 
    }
    public AuthenticationDto Authentication(LoginDto dto)
    {
        bool credentialsValid = false;

        var user = this.Repository.Find(c => c.Login.Email.Equals(dto.Email)).FirstOrDefault();
        if (user == null)
            throw new ArgumentException("Usuário inexistente!");
        else
        {
            credentialsValid = user != null && !String.IsNullOrEmpty(user.Login.Password) && !String.IsNullOrEmpty(user.Login.Email) && (_crypto.Decrypt(user.Login.Password).Equals(dto.Password));
        }
        
        if (credentialsValid)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Login.Email, "Login"),
                new[]
                {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login.Email),
                        new Claim("UserType", "Customer"),
                });

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = _singingConfiguration.CreateToken(identity, handler, user.Id, _tokenConfiguration);

            return new AuthenticationDto
            {
                AccessToken = token
            };

        }
        throw new ArgumentException("Usuário Inválido!");
    }
}