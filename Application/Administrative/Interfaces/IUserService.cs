using Application.Administrative.Dto;
using Application.Shared.Dto;

namespace Application.Administrative.Interfaces;
public interface IAuthenticationService
{
    AdministrativeAccountDto Authentication(LoginDto dto);
}
