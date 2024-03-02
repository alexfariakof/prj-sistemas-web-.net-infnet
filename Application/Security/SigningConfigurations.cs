using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Security;
public sealed class SigningConfigurations
{
    private static readonly Lazy<SigningConfigurations> lazyInstance =
        new Lazy<SigningConfigurations>(() => new SigningConfigurations());

    private TokenConfiguration _tokenConfiguration;
    public static SigningConfigurations Instance => lazyInstance.Value;
    public SecurityKey Key { get; }
    public SigningCredentials SigningCredentials { get; }

    public void setTokenConfigurations(TokenConfiguration tokenConfiguration) 
    {
        this._tokenConfiguration = tokenConfiguration;
    }
    private SigningConfigurations()
    {
        using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }
        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }
    public string CreateToken(ClaimsIdentity identity, JwtSecurityTokenHandler handler, Guid UserId)
    {
        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

        Microsoft.IdentityModel.Tokens.SecurityToken securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Issuer = _tokenConfiguration.Issuer,
            Audience = _tokenConfiguration.Audience,
            SigningCredentials = SigningConfigurations.Instance.SigningCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expirationDate,
            Claims = new Dictionary<string, object> { { "UserId", UserId } },
        });

        string token = handler.WriteToken(securityToken);
        return token;
    }
}
