using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Project.Infrastructure
{
    public static class ProjectIdentityItems
    {
        public const string AdminRole = "admin";
        public const string CustomerRole = "customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new(name: "space-scope", displayName: "custom scope"),
                new(name: "read", displayName: "You can read"),
                new(name: "write", displayName: "You can write"),
                new(name: "delete", displayName: "You can delete")
            };

        public static IEnumerable<Client> Clients =>
            [
                new() {
                        ClientId = "service.client",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        AllowedScopes = { "api1", "api2.read_only" }
                    },
                    new() {
                        ClientId = "space-client-Id",
                        ClientSecrets = { new Secret("newSecret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,
                        AllowedScopes =
                        {
                            "space-scope",
                            "read",
                            "write",
                            IdentityServerConstants.StandardScopes.OpenId
                        },
                        RedirectUris={ "https://localhost:7002/signin-oidc" },
                        PostLogoutRedirectUris={"https://localhost:7002/signout-callback-oidc" },
                    }
            ];
    }
}
