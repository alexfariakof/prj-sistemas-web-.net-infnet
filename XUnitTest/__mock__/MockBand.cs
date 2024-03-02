using Domain.Streaming.Agreggates;
using Bogus;

namespace __mock__;
public class MockBand
{
    private static MockBand _instance;
    private static readonly object LockObject = new object();

    private MockBand() { }

    public static MockBand Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockBand();
            }
        }
    }

    public Band GetFaker()
    {
        var fakeBand = new Faker<Band>()
            .RuleFor(b => b.Id, f => f.Random.Guid())
            .RuleFor(b => b.Name, f => f.Name.FullName())
            .RuleFor(b => b.Description, f => f.Lorem.Sentence())
            .RuleFor(b => b.Backdrop, f => f.Image.PlaceImgUrl())
            .RuleFor(b => b.Albums, f => MockAlbum.Instance.GetListFaker(2))
            .Generate();

        return fakeBand;
    }
}
