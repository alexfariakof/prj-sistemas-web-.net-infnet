using Application.Conta.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Repository.Repositories;

namespace Application.Account;
public class CustomerService
{
    private IMapper Mapper { get; set; }
    private CustomerRepository CustomerRepository { get; set; }
    private FlatRepository FlatRepository { get; set; }
    public CustomerService(IMapper mapper, CustomerRepository customerRepository, FlatRepository flatRepository)
    {
        Mapper = mapper;
        CustomerRepository = customerRepository;
        FlatRepository = flatRepository;
    }
    public CustomerDto Create(CustomerDto dto)
    {
        if (this.CustomerRepository.Exists(x => x.Login != null && x.Login.Email == dto.Email))
            throw new Exception("Usuario já existente na base");


        Flat flat = this.FlatRepository.GetById(dto.FlatId);

        if (flat == null)
            throw new Exception("Plano não existente ou não encontrado");

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
        
        customer.CreateAccount(customer, dto.Address ?? new(), flat, card);
        
        this.CustomerRepository.Save(customer);
        var result = this.Mapper.Map<CustomerDto>(customer);

        return result;
    }
    public CustomerDto FindById(Guid id)
    {
        var usuario = this.CustomerRepository.GetById(id);
        var result = this.Mapper.Map<CustomerDto>(usuario);
        return result;
    }
}