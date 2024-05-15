using Domain.Account.ValueObject;
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
    protected PerfilUser UserType
    {
        get
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = HttpContext.Request.Headers.Authorization.ToString();
                var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
                var userTypeClaim = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "UserType")?.Value;

                if (Enum.TryParse<PerfilUser.UserType>(userTypeClaim, out var userType))
                {
                    return userType;
                }

                return PerfilUser.UserType.Customer;
            }
            catch
            {
                return PerfilUser.UserType.Customer;
            }
        }
    }
}