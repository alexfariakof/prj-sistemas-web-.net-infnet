namespace Application.Streaming.Dto.Interfaces;
public interface IFlatService
{
    FlatDto Create(FlatDto obj);
    List<FlatDto> FindAll();
    FlatDto FindById(Guid id);
    FlatDto Update(FlatDto obj);
    bool Delete(FlatDto obj);
}