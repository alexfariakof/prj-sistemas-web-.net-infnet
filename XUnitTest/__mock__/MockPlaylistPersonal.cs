using Bogus;
using Domain.Account.Agreggates;

namespace __mock__;
public static class MockPlaylistPersonal
{
    public static PlaylistPersonal GetFaker()
    {
        var fakePlaylist = new Faker<PlaylistPersonal>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => f.Lorem.Word())
            .RuleFor(p => p.Musics, f => MockMusic.GetListFaker(3))
            .Generate();

        return fakePlaylist;
    }

    public static List<PlaylistPersonal> GetListFaker(int count)
    {
        var playlistList = new List<PlaylistPersonal>();
        for (var i = 0; i < count; i++)
        {
            playlistList.Add(GetFaker());
        }
        return playlistList;
    }
}