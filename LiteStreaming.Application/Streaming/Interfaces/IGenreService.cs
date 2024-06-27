namespace Application.Streaming.Dto.Interfaces;
public interface IGenreService
{
    GenreDto Create(GenreDto obj);
    List<GenreDto> FindAll();
    GenreDto FindById(Guid id);
    GenreDto Update(GenreDto obj);
    bool Delete(GenreDto obj);
}