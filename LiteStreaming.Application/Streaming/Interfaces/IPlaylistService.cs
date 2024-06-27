namespace Application.Streaming.Dto.Interfaces;
public interface IPlaylistService
{
    PlaylistDto Create(PlaylistDto obj);
    List<PlaylistDto> FindAll();
    PlaylistDto FindById(Guid id);
    PlaylistDto Update(PlaylistDto obj);
    bool Delete(PlaylistDto obj);
}