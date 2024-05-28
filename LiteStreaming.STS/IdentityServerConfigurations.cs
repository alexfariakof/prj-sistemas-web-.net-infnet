using IdentityServer4;
using IdentityServer4.Models;

namespace LiteStreaming.STS;

internal class IdentityServerConfigurations
{
    public static IEnumerable<IdentityResource> GetIdentityResource()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    }

    public static IEnumerable<ApiResource> GetApiResources() 
    {
        return new List<ApiResource>
        {
            new ApiResource("lite-streaming-webapi", "lite-streaming", new string[] {  "UserId" })
            {
                ApiSecrets =
                {
                    new Secret("LiteStreamingSecret".Sha256())
                },
                Scopes = 
                {
                    "LiteStreamingScope"
                }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>()
        {
            new ApiScope()
            {
                Name = "LiteStreamingScope",
                DisplayName = "Lite Streaming API",
                UserClaims = { "UserId" }
            }
        };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>()
        {
            new Client()
            {
                ClientId = "client-angular-lite-streaming",
                ClientName = "Acesso do Frontend a API",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                {
                    new Secret("LiteStreamingSecret".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "LiteStreamingScope"
                }
            }
        };
    }
}
