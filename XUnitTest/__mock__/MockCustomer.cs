using Domain.Account.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Account.ValueObject;

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
            .RuleFor(c => c.Addresses, MockAddress.GetListFaker(10))
            .Generate();

        return fakeCustomer;
    }
}