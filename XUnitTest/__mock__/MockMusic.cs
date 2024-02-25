using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Bogus;

namespace __mock__;
public class MockMusic
{
    public static Music GetFaker()
    {
        var fakeMusic = new Faker<Music>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => f.Commerce.ProductName())
            .RuleFor(m => m.Duration, f => new Duration(f.Random.Int(60, 600)))
            .RuleFor(m => m.Playlists, new List<Playlist>())
            .Generate();

        return fakeMusic;
    }

    public static List<Music> GetListFaker(int count)
    {
        var musicList = new List<Music>();
        for (var i = 0; i < count; i++)
        {
            musicList.Add(GetFaker());
        }
        return musicList;
    }
}