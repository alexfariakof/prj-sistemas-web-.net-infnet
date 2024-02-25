using Domain.Streaming.Agreggates;
using Bogus;

namespace __mock__;
public class MockAlbum
{
    public static Album GetFaker()
    {
        var fakeAlbum = new Faker<Album>()
          .RuleFor(a => a.Id, f => f.Random.Guid())
          .RuleFor(a => a.Name, f => f.Commerce.ProductName())
          .RuleFor(a => a.Music, f => MockMusic.GetListFaker(3))
           .Generate();

        return fakeAlbum;
    }
    public static List<Album> GetListFaker(int count)
    {
        var albumList = new List<Album>();
        for (var i = 0; i < count; i++)
        {
            albumList.Add(GetFaker());
        }
        return albumList;
    }
}