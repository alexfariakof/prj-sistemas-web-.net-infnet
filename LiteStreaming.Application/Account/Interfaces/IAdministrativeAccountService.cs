using Application.Administrative.Dto;

namespace Application.Administrative.Interfaces;
public interface IAdministrativeAccountService
{
    AdministrativeAccountDto Create(AdministrativeAccountDto obj);
    List<AdministrativeAccountDto> FindAll(Guid userId);
    List<AdministrativeAccountDto> FindAll();
    AdministrativeAccountDto FindById(Guid id);
    AdministrativeAccountDto Update(AdministrativeAccountDto obj);
    bool Delete(AdministrativeAccountDto obj);
    
}
