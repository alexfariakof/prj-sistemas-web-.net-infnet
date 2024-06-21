using Application.Administrative.Dto;
using Application.Shared.Dto;

namespace Application.Administrative.Interfaces;
public interface IAdministrativeAuthenticationService
{
    AdministrativeAccountDto Authentication(LoginDto dto);
}
