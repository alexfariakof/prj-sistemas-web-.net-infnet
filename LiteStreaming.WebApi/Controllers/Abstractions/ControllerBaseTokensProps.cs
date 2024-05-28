using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers.Abstractions;
public abstract class ControllerBaseTokensProps : ControllerBase
{
    protected ControllerBaseTokensProps() { }
    protected Guid UserIdentity
    {
        get
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = HttpContext.Request.Headers.Authorization.ToString();
                var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
                var userId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
                return new Guid(userId);
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}