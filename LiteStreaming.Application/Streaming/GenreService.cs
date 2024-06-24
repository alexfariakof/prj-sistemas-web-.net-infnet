using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;
using AutoMapper;
using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Application.Streaming;
public class GenreService : ServiceBase<GenreDto, Genre>, IService<GenreDto>, IGenreService
{
    public GenreService(IMapper mapper, IRepository<Genre> GenreRepository) : base(mapper, GenreRepository)  { }
    public override GenreDto Create(GenreDto dto)
    {
        if (Repository.Exists(x => x.Name != null && x.Name == dto.Name))
            throw new ArgumentException("Gênero já existente.");

        Genre Genre = Mapper.Map<Genre>(dto);
        Repository.Save(Genre);
        var result = Mapper.Map<GenreDto>(Genre);
        return result;
    }
    public override GenreDto FindById(Guid GenreId)
    {
        var Genre = Repository.GetById(GenreId);
        var result = Mapper.Map<GenreDto>(Genre);
        return result;
    }
    public List<GenreDto> FindAll()
    {
        var Genres = Repository.GetAll().ToList();
        var result = Mapper.Map<List<GenreDto>>(Genres);
        return result;
    }
        
    public override GenreDto Update(GenreDto dto)
    {
        var Genre = Mapper.Map<Genre>(dto);
        Repository.Update(Genre);
        return Mapper.Map<GenreDto>(Genre);
    }
    public override bool Delete(GenreDto dto)
    {
        var Genre = Mapper.Map<Genre>(dto);
        Repository.Delete(Genre);
        return true;
    }
}