using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using __mock__;

namespace Domain.Streaming;
public class MusicTest
{
    [Fact]
    public void Should_Set_Properties_Correctly_Music()
    {
        // Arrange
        var fakeMusic = MockMusic.GetFaker();

        // Act
        var music = new Music
        {
            Name = fakeMusic.Name,
            Duration = fakeMusic.Duration,
            Playlists = fakeMusic.Playlists
        };

        // Assert
        Assert.Equal(fakeMusic.Name, music.Name);
        Assert.Equal(fakeMusic.Duration, music.Duration);
        Assert.Equal(fakeMusic.Playlists, music.Playlists);
    }

    [Fact]
    public void Should_Set_Properties_Correctly_Playlist()
    {
        // Arrange
        var fakeMusic1 = MockMusic.GetFaker();
        var fakeMusic2 = MockMusic.GetFaker();
        var fakeMusicList = new List<Music> { fakeMusic1, fakeMusic2 };

        // Act
        var playlist = new Playlist
        {
            Name = "Test Playlist",
            Musics = fakeMusicList
        };

        // Assert
        Assert.Equal("Test Playlist", playlist.Name);
        Assert.Equal(fakeMusicList, playlist.Musics);
    }

    [Fact]
    public void Should_Set_Properties_Correctly_PlaylistPersonal()
    {
        // Arrange
        var fakeCustomer = MockCustomer.GetFaker();
        var fakeMusic = MockMusic.GetFaker();
        var fakeMusicList = new List<Music> { fakeMusic };

        // Act
        var playlistPersonal = new PlaylistPersonal
        {
            Customer = fakeCustomer,
            IsPublic = true,
            DtCreated = DateTime.Now,
            Name = "Personal Playlist",
            Musics = fakeMusicList
        };

        // Assert
        Assert.Equal(fakeCustomer, playlistPersonal.Customer);
        Assert.True(playlistPersonal.IsPublic);
        Assert.Equal("Personal Playlist", playlistPersonal.Name);
        Assert.Equal(fakeMusicList, playlistPersonal.Musics);
    }
}