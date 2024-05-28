using Application.Administrative.Dto;

namespace Application.Administrative.Interfaces;
public interface IAdministrativeAccountService
{
    AdministrativeAccountDto Create(AdministrativeAccountDto obj);
    List<AdministrativeAccountDto> FindAll(Guid userId);
    AdministrativeAccountDto FindById(Guid id);
    AdministrativeAccountDto Update(AdministrativeAccountDto obj);
    bool Delete(AdministrativeAccountDto obj);
    IEnumerable<AdministrativeAccountDto> FindAll();
}
