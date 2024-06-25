namespace Application.Streaming.Dto.Interfaces;
public interface IBandService
{
    BandDto Create(BandDto obj);
    List<BandDto> FindAll();
    BandDto FindById(Guid id);
    BandDto Update(BandDto obj);
    bool Delete(BandDto obj);
}