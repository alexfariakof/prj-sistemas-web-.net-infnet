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
public class MerchantService : ServiceBase<MerchantDto, Merchant>, IService<MerchantDto>, IMerchantService
{
    private readonly ICrypto _crypto = Crypto.GetInstance;
    private readonly SigningConfigurations _singingConfiguration;
    private readonly TokenConfiguration _tokenConfiguration;
    private readonly IRepository<Flat> _flatRepository;
    public MerchantService(IMapper mapper, IRepository<Merchant> merchantRepository, IRepository<Flat> flatRepository, SigningConfigurations singingConfiguration, TokenConfiguration tokenConfiguration) : base(mapper, merchantRepository)
    {
        _flatRepository = flatRepository;
        _singingConfiguration = singingConfiguration;
        _tokenConfiguration = tokenConfiguration;
    }
    public override MerchantDto Create(MerchantDto dto)
    {
        if (this.Repository.Exists(x => x.Customer.Login != null && x.Customer.Login.Email == dto.Email))
            throw new ArgumentException("Usuário já existente na base.");


        Flat flat = this._flatRepository.GetById(dto.FlatId);

        if (flat == null)
            throw new ArgumentException("Plano não existente ou não encontrado.");

        Card card = this.Mapper.Map<Card>(dto.Card);

        Merchant merchant = new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CNPJ = dto.CNPJ,
            Customer = new()
            {
                Name = dto.Name,
                CPF = dto.CPF,
                Phone = dto.Phone,
                Login = new()
                {
                    Email = dto.Email ?? "",
                    Password = dto.Password ?? ""
                }
            }
        };

        Address address = this.Mapper.Map<Address>(dto.Address);

        merchant.CreateAccount(merchant, address, flat, card);

        this.Repository.Save(merchant);
        var result = this.Mapper.Map<MerchantDto>(merchant);

        return result;
    }
    public override MerchantDto FindById(Guid id)
    {
        var merchant = this.Repository.GetById(id);
        var result = this.Mapper.Map<MerchantDto>(merchant);
        return result;
    }

    public override List<MerchantDto> FindAll(Guid userId)
    {
        var merchants = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
        var result = this.Mapper.Map<List<MerchantDto>>(merchants);
        return result;
    }

    public override MerchantDto Update(MerchantDto dto)
    {
        var merchant = this.Mapper.Map<Merchant>(dto);
        this.Repository.Update(merchant);
        return this.Mapper.Map<MerchantDto>(merchant);
    }

    public override bool Delete(MerchantDto dto)
    {
        var merchant = this.Mapper.Map<Merchant>(dto);
        this.Repository.Delete(merchant);
        return true;
    }
    public AuthenticationDto Authentication(LoginDto dto)
    {
        bool credentialsValid = false;

        var user = this.Repository.Find(c => c.Customer.Login.Email.Equals(dto.Email)).FirstOrDefault();
        if (user == null)
            throw new ArgumentException("Usuário inexistente!");
        else
        {
            credentialsValid = user != null && !String.IsNullOrEmpty(user.Customer.Login.Password) && !String.IsNullOrEmpty(user.Customer.Login.Email) && (_crypto.Decrypt(user.Customer.Login.Password).Equals(dto.Password));
        }

        if (credentialsValid)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Customer.Login.Email, "Login"),
                new[]
                {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Customer.Login.Email),
                        new Claim("UserType", "Merchant"),
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