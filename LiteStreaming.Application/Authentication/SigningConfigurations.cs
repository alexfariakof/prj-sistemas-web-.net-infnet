using Application.Authentication.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Authentication;
public class SigningConfigurations
{
    public readonly SecurityKey Key;
    public readonly SigningCredentials SigningCredentials;
    public readonly ITokenConfiguration TokenConfiguration;

    public SigningConfigurations(IOptions<TokenOptions> options)
    {
        TokenConfiguration = new TokenConfiguration(options);
        using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }

        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }

    public string CreateAccessToken(ClaimsIdentity identity)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = this.TokenConfiguration.Issuer,
            Audience = this.TokenConfiguration.Audience,
            SigningCredentials = this.SigningCredentials,
            Subject = identity,
            NotBefore = DateTime.Now,
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddSeconds(this.TokenConfiguration.Seconds),
        });

        string token = handler.WriteToken(securityToken);
        return token;
    }
}
