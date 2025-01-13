using Duende.IdentityServer.Models;

namespace Project.Identity.Client
{
    public static class IdentitySeed
    {
        public const string Admin = "admin";
        public const string Customer = "customer";

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

        public static IEnumerable<Duende.IdentityServer.Models.Client> Clients =>
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
                        AllowedScopes = { "space-scope", "read", "write", "delete" }
                    }
            ];
    }
}
