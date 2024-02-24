using Domain.Streaming.Agreggates;
using __mock__;

namespace Domain.Streaming;
public class PlaylistTest
{
    [Fact]
    public void Should_Set_Properties_Correctly_Playlist()
    {
        // Arrange
        var fakePlaylist = MockPlaylist.GetFaker();

        // Act
        var playlist = new Playlist
        {
            Id = fakePlaylist.Id,
            Name = fakePlaylist.Name,
            Flat = fakePlaylist.Flat,
            Musics = fakePlaylist.Musics
        };

        // Assert
        Assert.Equal(fakePlaylist.Id, playlist.Id);
        Assert.Equal(fakePlaylist.Name, playlist.Name);
        Assert.Equal(fakePlaylist.Flat, playlist.Flat);
        Assert.Equal(fakePlaylist.Musics, playlist.Musics);
    }

    [Fact]
    public void Should_Add_Music_Correctly_Playlist()
    {
        // Arrange
        var playlist = new Playlist();
        var fakeMusic = MockMusic<Playlist>.GetFaker();
        var fakeMusicList = MockMusic<Playlist>.GetListFaker(2);

        // Act
        playlist.Musics.Add(fakeMusic);
        playlist.Musics.AddRange(fakeMusicList);

        // Assert
        Assert.Single(playlist.Musics, fakeMusic);
        Assert.True(fakeMusicList.Count < playlist.Musics.Count); 
    }
}