using System.Security.Claims;

namespace Application.Authentication.Abstractions;
public interface ISigningConfigurations
{
    string CreateAccessToken(ClaimsIdentity identity);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken);
}
