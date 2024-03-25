using Application.Account.Dto;
using Application.Account.Dto.Interfaces;
using AutoMapper;
using Domain.Account.Agreggates;
using Repository.Interfaces;

namespace Application.Account;
public class PlaylistPersonalService : ServiceBase<PlaylistPersonalDto, PlaylistPersonal>, IService<PlaylistPersonalDto>, IPlaylistPersonalService
{
    public PlaylistPersonalService(IMapper mapper, IRepository<PlaylistPersonal> playlistPersonalRepository) : base(mapper, playlistPersonalRepository)  { }
    public override PlaylistPersonalDto Create(PlaylistPersonalDto dto)
    {
        if (Repository.Exists(x => x.Name != null && x.Name == dto.Name))
            throw new ArgumentException("Playlist já existente.");

        PlaylistPersonal playlist = Mapper.Map<PlaylistPersonal>(dto);

        Repository.Save(playlist);
        var result = Mapper.Map<PlaylistPersonalDto>(playlist);
        return result;
    }
    public override PlaylistPersonalDto FindById(Guid PlaylistId)
    {
        var playlist = Repository.GetById(PlaylistId);
        var result = Mapper.Map<PlaylistPersonalDto>(playlist);
        return result;
    }

    public override List<PlaylistPersonalDto> FindAll(Guid userId)
    {
        var playlists = Repository.GetAll().ToList();
        var result = Mapper.Map<List<PlaylistPersonalDto>>(playlists);
        return result;
    }
    public override PlaylistPersonalDto Update(PlaylistPersonalDto dto)
    {
        var playlist = Mapper.Map<PlaylistPersonal>(dto);
        Repository.Update(playlist);
        return Mapper.Map<PlaylistPersonalDto>(playlist);
    }
    public override bool Delete(PlaylistPersonalDto dto)
    {
        var Playlist = Mapper.Map<PlaylistPersonal>(dto);
        Repository.Delete(Playlist);
        return true;
    }
}