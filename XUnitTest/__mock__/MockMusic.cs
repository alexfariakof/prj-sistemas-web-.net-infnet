using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Bogus;

namespace __mock__;
public class MockMusic<T>
{
    public static Music<T> GetFaker()
    {
        var fakeMusic = new Faker<Music<T>>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => f.Commerce.ProductName())
            .RuleFor(m => m.Duration, f => new Duration(f.Random.Int(60, 600)))
            .RuleFor(m => m.Playlists, new List<T>())
            .Generate();

        return fakeMusic;
    }

    public static List<Music<T>> GetListFaker(int count)
    {
        var musicList = new List<Music<T>>();
        for (var i = 0; i < count; i++)
        {
            musicList.Add(GetFaker());
        }
        return musicList;
    }
}