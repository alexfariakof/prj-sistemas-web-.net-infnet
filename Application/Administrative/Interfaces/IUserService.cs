using Application.Shared.Dto;

namespace Application.Administrative.Interfaces;
public interface IAuthenticationService
{    bool Authentication(LoginDto dto);
}
