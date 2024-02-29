using Application.Account.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Repository;

namespace Application.Account;
public class CustomerService : ServiceBase<CustomerDto, Customer>, IService<CustomerDto>
{
    private IRepository<Flat> FlatRepository { get; set; }
    public CustomerService(IMapper mapper, IRepository<Customer> customerRepository, IRepository<Flat> flatRepository) : base(mapper, customerRepository)
    {
        FlatRepository = flatRepository;
    }
    public override CustomerDto Create(CustomerDto dto)
    {
        if (this.Repository.Exists(x => x.Login != null && x.Login.Email == dto.Email))
            throw new Exception("Usuário já existente na base.");


        Flat flat = this.FlatRepository.GetById(dto.FlatId);

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
}