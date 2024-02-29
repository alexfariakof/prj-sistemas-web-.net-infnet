using Domain.Account.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Account.ValueObject;
using Domain.Transactions.Agreggates;

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
}