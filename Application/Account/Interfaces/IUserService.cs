using Application.Account.Dto;
using Application.Shared.Dto;

namespace Application.Account.Interfaces;
public interface IUserService
{    AuthenticationDto Authentication(LoginDto dto);
}
