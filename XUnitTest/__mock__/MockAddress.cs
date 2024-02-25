using Bogus;
using Domain.Account.ValueObject;

namespace __mock__;
public class MockAddress
{
    public static Adress GetFaker()
    {
        var fakeAddress = new Faker<Adress>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(a => a.Zipcode, f => f.Address.ZipCode())
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
            .RuleFor(a => a.Neighborhood, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.State, f => f.Address.State())
            .RuleFor(a => a.Complement, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .Generate();

        return fakeAddress;
    }

    public static List<Adress> GetListFaker(int count)
    {
        var addressList = new List<Adress>();
        for (var i = 0; i < count; i++)
        {
            addressList.Add(GetFaker());
        }
        return addressList;
    }

}