using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class IdentityServer4TestConfig
    {
        public static IEnumerable<ApiScope> ApiScopes =>
           new List<ApiScope>
           {
                new ApiScope("api1", "My API")
           };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    RedirectUris=new List<string>{"https://localhost:5002"},

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()) //访问时用secret明文
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1","email" },
                    //AllowedCorsOrigins=new List<string>{""}, // 在idserver的cors列表里添加此client的orign地址，如https://localhost:5002
                },
                new Client
                {
                    ClientId = "mvc",
                    RedirectUris=new List<string>{"https://localhost:5002/signin-oidc"},

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()) //访问时用secret明文
                    },

                    // scopes that client has access to
                    //AllowedCorsOrigins=new List<string>{""}, // 在idserver的cors列表里添加此client的orign地址，如https://localhost:5002\
                       AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };


        public static IEnumerable<IdentityResource> IdentityResources =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResource(
                name: "email",
                userClaims: new[] { "email" },
                displayName: "Your user identifier")
          };

        public static List<TestUser> Users =>
         new List<TestUser>
         {
             new TestUser{Username="shengyu",Password="123456",SubjectId="userid123456",ProviderName="identityserver4",ProviderSubjectId="ss1234",Claims=new List<Claim>{ new Claim("email","bradyzhou@tencent.com")} }
         };
    }
}
