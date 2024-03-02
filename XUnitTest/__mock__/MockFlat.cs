using Domain.Streaming.Agreggates;
using Bogus;
using Domain.Core.ValueObject;

namespace __mock__;
public class MockFlat
{
    private static MockFlat _instance;
    private static readonly object LockObject = new object();

    private readonly Faker<Flat> _faker;

    private MockFlat()
    {
        _faker = new Faker<Flat>()
            .RuleFor(f => f.Id, f => f.Random.Guid())
            .RuleFor(f => f.Name, f => f.Commerce.ProductName())
            .RuleFor(f => f.Description, f => f.Lorem.Sentence())
            .RuleFor(f => f.Value, f => new Monetary(f.Finance.Amount()));
    }

    public static MockFlat Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new MockFlat();
                    }
                }
            }
            return _instance;
        }
    }

    public Flat GetFaker()
    {
        return _faker.Generate();
    }

    public List<Flat> GetListFaker(int count)
    {
        var flatList = new List<Flat>();
        for (var i = 0; i < count; i++)
        {
            flatList.Add(GetFaker());
        }
        return flatList;
    }
}