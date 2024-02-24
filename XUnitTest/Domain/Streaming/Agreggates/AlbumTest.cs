using Domain.Streaming.Agreggates;
using Domain.Account.Agreggates;
using __mock__;

namespace Domain.Streaming;
public class AlbumTest
{
    [Fact]
    public void Should_Set_Properties_Correctly_Album()
    {
        // Arrange
        var fakeAlbum = MockAlbum.GetFaker();

        // Act
        var album = new Album
        {
            Id = fakeAlbum.Id,
            Name = fakeAlbum.Name,
            Music = fakeAlbum.Music
        };

        // Assert
        Assert.Equal(fakeAlbum.Id, album.Id);
        Assert.Equal(fakeAlbum.Name, album.Name);
        Assert.Equal(fakeAlbum.Music, album.Music);
    }

    [Fact]
    public void Should_Add_Music_Correctly_Album()
    {
        // Arrange
        var album = new Album();
        var fakeMusic = MockMusic<Playlist>.GetFaker();
        var fakeMusicList = MockMusic<Playlist>.GetListFaker(2);

        // Act
        album.AddMusic(fakeMusic);
        album.AddMusic(fakeMusicList);

        // Assert
        Assert.Single(album.Music, fakeMusic);
        Assert.True(fakeMusicList.Count < album.Music.Count);
    }

    [Fact]
    public void Should_Add_Music_Personal_Correctly_Album()
    {
        // Arrange
        var album = new Album();
        var fakeMusic = MockMusic<PlaylistPersonal>.GetFaker();
        var fakeMusicList = MockMusic<PlaylistPersonal>.GetListFaker(2);

        // Act
        album.AddMusic(fakeMusic);
        album.AddMusic(fakeMusicList);

        // Assert
        Assert.Single(album.MusicPersonal, fakeMusic);
        Assert.True(fakeMusicList.Count < album.MusicPersonal.Count);
    }
}