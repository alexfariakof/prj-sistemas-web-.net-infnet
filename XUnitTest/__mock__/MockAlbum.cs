using Domain.Streaming.Agreggates;
using Bogus;

namespace __mock__;
public class MockAlbum
{
    private static readonly Lazy<MockAlbum> instance = new Lazy<MockAlbum>(() => new MockAlbum());

    public static MockAlbum Instance => instance.Value;

    private MockAlbum() { }

    public Album GetFaker()
    {
        var fakeAlbum = new Faker<Album>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Name, f => f.Commerce.ProductName())
            .RuleFor(a => a.Music, f => MockMusic.GetListFaker(3))
            .Generate();

        return fakeAlbum;
    }

    public List<Album> GetListFaker(int count)
    {
        var albumList = new List<Album>();
        for (var i = 0; i < count; i++)
        {
            albumList.Add(GetFaker());
        }
        return albumList;
    }
}