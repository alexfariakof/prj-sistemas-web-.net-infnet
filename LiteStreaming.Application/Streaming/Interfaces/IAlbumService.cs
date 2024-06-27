namespace Application.Streaming.Dto.Interfaces;
public interface IAlbumService
{
    AlbumDto Create(AlbumDto obj);
    List<AlbumDto> FindAll();
    AlbumDto FindById(Guid id);
    AlbumDto Update(AlbumDto obj);
    bool Delete(AlbumDto obj);
}