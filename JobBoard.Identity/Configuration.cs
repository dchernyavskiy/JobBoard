using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace JobBoard.Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =
            new List<ApiScope> { new ApiScope("JobBoardWebApi", "Web Api") };

        public static IEnumerable<IdentityResource> IdentityResources =
            new List<IdentityResource> { new IdentityResources.OpenId(), new IdentityResources.Profile() };

        public static IEnumerable<ApiResource> ApiResources =
            new List<ApiResource> {
                new ApiResource("JobBoardWebApi", "Web Api", new[] { JwtClaimTypes.Name, JwtClaimTypes.Role }) { Scopes = { "JobBoardWebApi" } }
            };

        public static IEnumerable<Client> Clients =
            new List<Client> {
                new Client{
                    ClientId = "job-board-web-app",
                    ClientName = "JobBoard Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret  = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:4200"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4200"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JobBoardWebApi"
                    },
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = "job-board-android-app",
                    ClientName = "JobBoard Android",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = { "com.example.jobboard://oidccallback" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "JobBoardWebApi"
                    },
                    AllowAccessTokensViaBrowser = true,
                },
            };
    }
}