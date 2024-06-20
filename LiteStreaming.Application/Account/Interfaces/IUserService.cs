using Application.Streaming.Dto;
using Application.Shared.Dto;

namespace Application.Streaming.Interfaces;
public interface IUserService
{    AuthenticationDto Authentication(LoginDto dto);
}
