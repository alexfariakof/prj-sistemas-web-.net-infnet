using Application.Core;

namespace Application.Administrative.Interfaces;
public interface IAuthenticationService
{    bool Authentication(LoginDto dto);
}
