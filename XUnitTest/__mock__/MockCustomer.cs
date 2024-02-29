using Domain.Account.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Account.ValueObject;
using Domain.Transactions.Agreggates;
using Application.Account.Dto;

namespace __mock__;
public static class MockCustomer
{
    public static Customer GetFaker()
    {
        var fakeCustomer = new Faker<Customer>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Login, MockLogin.GetFaker())
            .RuleFor(c => c.CPF, f => f.Person.Cpf())
            .RuleFor(c => c.Birth, f => f.Person.DateOfBirth)
            .RuleFor(c => c.Phone, f => new Phone { Number = f.Person.Phone })
            .RuleFor(c => c.Addresses, MockAddress.GetListFaker(1))
            .RuleFor(m => m.Cards, f => new List<Card>())
            .RuleFor(m => m.Signatures, f => new List<Signature>())
            .Generate();

        return fakeCustomer;
    }
    public static List<Customer> GetListFaker(int count)
    {
        var customerList = new List<Customer>();
        for (var i = 0; i < count; i++)
        {
            customerList.Add(GetFaker());
        }
        return customerList;
    }

    public static CustomerDto GetDtoFromCustomer(Customer customer)
    {
        var fakeCustomerDto = new Faker<CustomerDto>()
            .RuleFor(c => c.Id, f => customer.Id)
            .RuleFor(c => c.Name, f => customer.Name)
            .RuleFor(c => c.Email, f => customer.Login.Email)
            .RuleFor(c => c.Password, f => customer.Login.Password)
            .RuleFor(c => c.CPF, f => customer.CPF)
            .RuleFor(c => c.Birth, f => customer.Birth)
            .RuleFor(c => c.Phone, f => customer.Phone.Number)
            .RuleFor(c => c.Address, f => MockAddress.GetDtoFromAddress(customer.Addresses[0]))
            .RuleFor(c => c.FlatId, f => Guid.NewGuid()) 
            .Generate();

        return fakeCustomerDto;
    }

    public static List<CustomerDto> GetDtoListFromCustomerList(List<Customer> customers)
    {
        var customerDtoList = new List<CustomerDto>();

        foreach (var customer in customers)
        {
            var customerDto = GetDtoFromCustomer(customer);
            customerDtoList.Add(customerDto);
        }

        return customerDtoList;
    }
}