using Domain.Streaming.Agreggates;
using Bogus;

namespace __mock__;
public class MockBand
{
    public static Band GetFaker()
    {
        var fakeBand = new Faker<Band>()
            .RuleFor(b => b.Id, f => f.Random.Guid())
            .RuleFor(b => b.Name, f => f.Name.FullName())
            .RuleFor(b => b.Description, f => f.Lorem.Sentence())
            .RuleFor(b => b.Backdrop, f => f.Image.PlaceImgUrl())
            .RuleFor(b => b.Albums, f => MockAlbum.GetListFaker(2))
            .Generate();

        return fakeBand;
    }
}