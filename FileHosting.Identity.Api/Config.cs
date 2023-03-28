using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FileHosting.Identity.Api;

public static class Config
{
    private const string StorageApiScopeName = "FileHosting.Storage.Api";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(StorageApiScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "FileHosting.Web",
                ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:6001/signin-oidc", "urn:ietf:wg:oauth:2.0:oob"},
                FrontChannelLogoutUri = "https://localhost:6001/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:6001/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    StorageApiScopeName
                }
            }
        };
}