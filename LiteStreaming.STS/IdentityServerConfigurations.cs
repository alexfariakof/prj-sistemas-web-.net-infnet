using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Tokens;

namespace LiteStreaming.STS;

internal class IdentityServerConfigurations
{
    const string API_RESOURCE_NAME = "lite-streaming-api-resource";
    const string DISPLAY_API_RESOURCE_NAME = "LiteStreamingResource";
    static readonly string[] USER_CLAIMS = { "openid", "profile", "email", "userid", "role" };
    const string SCOPE_NAME = "lite-streaming-scope";
    const string DISPLAY_NAME_SCOPE = "LiteStreamingScope";
    const string SECRET = "lite-streaming-secret";
    const string CLIENT_ID = "client-angular-lite-streaming";
    const string CLIENT_NAME = "Frontend Angular Application Lite Streaming";

    public static IEnumerable<IdentityResource> GetIdentityResource()
    {
        return new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }

    public static IEnumerable<ApiResource> GetApiResources() 
    {
        return new List<ApiResource>()
        {
            new ApiResource(API_RESOURCE_NAME , DISPLAY_API_RESOURCE_NAME, USER_CLAIMS )
            {
                ApiSecrets =
                {
                    new Secret(SECRET.Sha256())
                },
                Scopes = { "lite-streaming-scope" },
                AllowedAccessTokenSigningAlgorithms = { SecurityAlgorithms.RsaSha256 }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return
        [
            new ApiScope()
            {
                Name = SCOPE_NAME,
                DisplayName = DISPLAY_NAME_SCOPE,
                UserClaims = USER_CLAIMS
            }
        ];
    }

    
    public static IEnumerable<Client> GetClients()
    {
        return
        [
            new Client()
            {
                ClientId = CLIENT_ID,
                ClientName = CLIENT_NAME,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                {
                    new Secret(SECRET.Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.AccessTokenAudience,                    
                    SCOPE_NAME
                },
                RedirectUris = { "http://localhost:5055/signin-oidc", "https://localhost:7199/signin-oidc" },
                RefreshTokenExpiration = TokenExpiration.Sliding,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
            }
        ];
    }
}
