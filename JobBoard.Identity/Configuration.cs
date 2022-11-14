using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Identity
{
    public class Configuration
    {
        public static string WebClientUri = "http://localhost:4200";

        public static IEnumerable<ApiScope> ApiScopes =
            new List<ApiScope> { new ApiScope("JobBoardWebApi", "Web Api") };

        public static IEnumerable<IdentityResource> IdentityResources =
            new List<IdentityResource> { new IdentityResources.OpenId(), new IdentityResources.Profile() };

        public static IEnumerable<ApiResource> ApiResources =
            new List<ApiResource> { new ApiResource("JobBoardWebApi", "Web Api", new[] { JwtClaimTypes.Name }) { Scopes = { "JobBoardWebApi" } } };

        public static IEnumerable<Client> Clients =
            new List<Client> {
                new Client{
                    ClientId = "job-board-web-app",
                    ClientName = "JobBoard Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret  = false,
                    RequirePkce = true,
                    RedirectUris = { WebClientUri },
                    AllowedCorsOrigins = { WebClientUri },
                    PostLogoutRedirectUris = { WebClientUri },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
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
                    RedirectUris = { "com.yourcompany.yourapp://oidccallback" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "JobBoardWebApi"
                    }
                },
            };
    }
}
