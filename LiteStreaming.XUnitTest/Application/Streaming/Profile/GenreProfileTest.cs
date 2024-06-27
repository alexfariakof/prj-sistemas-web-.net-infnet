using Application.Streaming.Dto;
using AutoMapper;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public sealed class GenreProfileTest
{
    [Fact]
    public void Map_Genre_To_GenreDto_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<GenreProfile>(); }));

        var genre = MockGenre.Instance.GetFaker();

        // Act
        var genreDto = mapper.Map<GenreDto>(genre);

        // Assert
        Assert.NotNull(genre);
        Assert.Equal(genreDto.Id, genre.Id);
        Assert.Equal(genreDto.Name, genre.Name);
    }

    [Fact]
    public void Map_GenreDto_To_Genre_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<GenreProfile>(); }));

        var fakeUser = MockUser.Instance.GetFaker();
        var genreDto = MockGenre.Instance.GetFakerDto(fakeUser.Id);

        // Act
        var genre = mapper.Map<Genre>(genreDto);

        // Assert
        Assert.NotNull(genre);
        Assert.Equal(genreDto.Id, genre.Id);
        Assert.Equal(genreDto.Name, genre.Name);
    }
}
