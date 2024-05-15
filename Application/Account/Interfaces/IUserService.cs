using Application.Account.Dto;
using Application.Core;

namespace Application.Account.Interfaces;
public interface IUserService
{    AuthenticationDto Authentication(LoginDto dto);
}
