using Application.Streaming.Dto;
using Bogus;
using Domain.Streaming.Agreggates;

namespace __mock__;
public class MockGenre
{
    private static readonly Lazy<MockGenre> _instance = new Lazy<MockGenre>(() => new MockGenre());

    public static MockGenre Instance => _instance.Value;

    private MockGenre() { }

    public Genre GetFaker()
    {
        var fakeGenre = new Faker<Genre>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(a => a.Name, f => f.Music.Genre())
            .Generate();

        return fakeGenre;
    }

    public List<Genre> GetListFaker(int count = 3)
    {
        var genreList = new List<Genre>();
        for (var i = 0; i < count; i++)
        {
            genreList.Add(GetFaker());
        }
        return genreList;
    }

    public GenreDto GetFakerDto(Guid? idUsuario = null)
    {
        var fakeGenreDto = new Faker<GenreDto>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Name, f => f.Name.FirstName())
            .Generate();
        fakeGenreDto.UsuarioId = idUsuario == null ? fakeGenreDto.Id : idUsuario.Value;
        return fakeGenreDto;
    }

    public GenreDto GetDtoFromGenre(Genre genre)
    {
        var fakeGenreDto = new Faker<GenreDto>()
            .RuleFor(b => b.Id, f => genre.Id)
            .RuleFor(b => b.Name, f => genre.Name)
            .Generate();
        return fakeGenreDto;
    }

    public List<GenreDto> GetDtoListFromGenreList(IList<Genre> genres)
    {
        var genreDtoList = genres.Select(GetDtoFromGenre).ToList();
        return genreDtoList;
    }
}
