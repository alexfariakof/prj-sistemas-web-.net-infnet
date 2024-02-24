using Domain.Streaming.Agreggates;
using __mock__;

namespace Domain.Streaming;
public class BandTest
{
    [Fact]
    public void Band_Should_Set_Properties_Correctly()
    {
        // Arrange
        var fakeBand = MockBand.GetFaker();

        // Act
        var band = new Band
        {
            Id = fakeBand.Id,
            Name = fakeBand.Name,
            Description = fakeBand.Description,
            Backdrop = fakeBand.Backdrop,
            Albums = fakeBand.Albums
        };

        // Assert
        Assert.Equal(fakeBand.Id, band.Id);
        Assert.Equal(fakeBand.Name, band.Name);
        Assert.Equal(fakeBand.Description, band.Description);
        Assert.Equal(fakeBand.Backdrop, band.Backdrop);
        Assert.Equal(fakeBand.Albums, band.Albums);
    }

    [Fact]
    public void Band_Should_Add_Album_Correctly()
    {
        // Arrange
        var band = new Band();
        var fakeAlbum = MockAlbum.GetFaker();
        var fakeAlbumList = MockAlbum.GetListFaker(2);

        // Act
        band.AddAlbum(fakeAlbum);
        band.Albums.AddRange(fakeAlbumList);

        // Assert
        Assert.Single(band.Albums, fakeAlbum);
        Assert.True(fakeAlbumList.Count < band.Albums.Count);
    }
}