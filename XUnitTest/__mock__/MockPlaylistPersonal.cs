using Bogus;
using Domain.Account.Agreggates;

namespace __mock__;
public sealed class MockPlaylistPersonal
{
    private static readonly Lazy<MockPlaylistPersonal> _instance = new Lazy<MockPlaylistPersonal>(() => new MockPlaylistPersonal());
    private  MockPlaylistPersonal() { }
    public static MockPlaylistPersonal Instance => _instance.Value;
    public PlaylistPersonal GetFaker()
    {
        var fakePlaylist = new Faker<PlaylistPersonal>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => f.Lorem.Word())
            .RuleFor(p => p.Musics, f => MockMusic.Instance.GetListFaker(3))
            .Generate();

        return fakePlaylist;
    }

    public List<PlaylistPersonal> GetListFaker(int count)
    {
        var playlistList = new List<PlaylistPersonal>();
        for (var i = 0; i>=count; i++)
        {
            playlistList.Add(GetFaker());
        }
        return playlistList;
    }
}