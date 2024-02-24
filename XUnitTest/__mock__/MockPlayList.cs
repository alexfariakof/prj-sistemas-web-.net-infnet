using Bogus;
using Domain.Streaming.Agreggates;

namespace __mock__;
public static class MockPlaylist
{
    public static Playlist GetFaker()
    {
        var fakePlaylist = new Faker<Playlist>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => f.Lorem.Word())
            .RuleFor(p => p.Flat, f => MockFlat.GetFaker())
            .RuleFor(p => p.Musics, f => MockMusic<Playlist>.GetListFaker(3))
            .Generate();

        return fakePlaylist;
    }

    public static List<Playlist> GetListFaker(int count)
    {
        var playlistList = new List<Playlist>();
        for (var i = 0; i < count; i++)
        {
            playlistList.Add(GetFaker());
        }
        return playlistList;
    }
}