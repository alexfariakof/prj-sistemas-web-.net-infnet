namespace Application.Streaming.Dto.Interfaces;
public interface IMusicService
{
    MusicDto Create(MusicDto obj);
    List<MusicDto> FindAll();
    MusicDto FindById(Guid id);
    MusicDto Update(MusicDto obj);
    bool Delete(MusicDto obj);
}