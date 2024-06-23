using IdentityServer4;
using IdentityServer4.Models;

namespace LiteStreaming.STS;

internal class IdentityServerConfigurations
{
    const string API_RESOURCE_NAME = "lite-streaming-webapi";
    const string DISPLAY_API_RESOURCE_NAME = "LiteStreamingResource";
    static readonly string[] USER_CLAIMS = ["UserId", "role"];
    static readonly string[] SCOPES = ["lite-streaming-scopes"];
    const string SCOPE_NAME = "lite-streaming-scope";
    const string DISPLAY_NAME_SCOPE = "LiteStreamingScope";
    const string SECRET = "lite-streaming-secret";
    const string CLIENT_ID = "client-angular-lite-streaming";
    const string CLIENT_NAME = "FrontEnd Angular Application Lite Streaming";


    public static IEnumerable<IdentityResource> GetIdentityResource()
    {
        return
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];
    }

    public static IEnumerable<ApiResource> GetApiResources() 
    {
        return
        [
            new ApiResource(API_RESOURCE_NAME , DISPLAY_API_RESOURCE_NAME, USER_CLAIMS )
            {
                ApiSecrets =
                {
                    new Secret(SECRET.Sha256())
                },
                Scopes = SCOPES 
            }
        ];
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
                    SCOPE_NAME
                }
            }
        ];
    }
}
