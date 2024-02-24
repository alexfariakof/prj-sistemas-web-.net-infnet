using Domain.Streaming.Agreggates;
using Bogus;
using Domain.Core.ValueObject;

namespace __mock__;
public class MockFlat
{
    public static Flat GetFaker()
    {
        var fakeFlat = new Faker<Flat>()
            .RuleFor(f => f.Id, f => f.Random.Guid())
            .RuleFor(f => f.Name, f => f.Commerce.ProductName())
            .RuleFor(f => f.Description, f => f.Lorem.Sentence())
            .RuleFor(f => f.Value, f => new Monetary(f.Finance.Amount()))
            .Generate();

        return fakeFlat;
    }
}