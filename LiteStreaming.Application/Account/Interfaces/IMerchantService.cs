using Application.Streaming.Dto;

namespace Application.Streaming.Interfaces;
public interface IMerchantService
{
    MerchantDto Create(MerchantDto obj);
    List<MerchantDto> FindAll(Guid userId);
    MerchantDto FindById(Guid id);
    MerchantDto Update(MerchantDto obj);
    bool Delete(MerchantDto obj);
}
