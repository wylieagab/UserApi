using Duende.IdentityServer.Models;

namespace IdentityServer.Config
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("userapi", "User Api")
                {
                    Scopes =
                    {
                        "userapi.fullaccess"
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("userapi.fullaccess"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "UserApi Client",
                    ClientId = "userapiclient",
                    ClientSecrets = { new Secret("035d330b-4c3b-4d3e-a534-a72d7e986377".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "userapi.fullaccess" },
                }
            };
    }
}
