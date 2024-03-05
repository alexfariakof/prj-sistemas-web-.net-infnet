using Application.Account.Dto;
using Application.Streaming.Dto.Interfaces;
using AutoMapper;
using Domain.Account.Agreggates;
using Repository;

namespace Application.Streaming;
public class PlaylistPesonalService : ServiceBase<PlaylistPersonalDto, PlaylistPersonal>, IService<PlaylistPersonalDto>, IPlaylistPersonalService
{
    public PlaylistPesonalService(IMapper mapper, IRepository<PlaylistPersonal> playlistPersonalRepository) : base(mapper, playlistPersonalRepository)  { }
    public override PlaylistPersonalDto Create(PlaylistPersonalDto dto)
    {
        if (Repository.Exists(x => x.Name != null && x.Name == dto.Name))
            throw new ArgumentException("Playlist já existente.");

        PlaylistPersonal Playlist = Mapper.Map<PlaylistPersonal>(dto);

        Repository.Save(Playlist);
        var result = Mapper.Map<PlaylistPersonalDto>(Playlist);
        return result;
    }
    public override PlaylistPersonalDto FindById(Guid PlaylistId)
    {
        var Playlist = Repository.GetById(PlaylistId);
        var result = Mapper.Map<PlaylistPersonalDto>(Playlist);
        return result;
    }

    public override List<PlaylistPersonalDto> FindAll(Guid userId)
    {
        var Playlists = Repository.GetAll().ToList();
        var result = Mapper.Map<List<PlaylistPersonalDto>>(Playlists);
        return result;
    }
    public override PlaylistPersonalDto Update(PlaylistPersonalDto dto)
    {
        var Playlist = Mapper.Map<PlaylistPersonal>(dto);
        Repository.Update(Playlist);
        return Mapper.Map<PlaylistPersonalDto>(Playlist);
    }
    public override bool Delete(PlaylistPersonalDto dto)
    {
        var Playlist = Mapper.Map<PlaylistPersonal>(dto);
        Repository.Delete(Playlist);
        return true;
    }
}