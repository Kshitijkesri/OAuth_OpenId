using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetwork.OAuth.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[] {
                new ApiResource("authserver", "Auth Server")
                {
                    UserClaims=new[]{ "email"}
                }
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new IdentityResource[] {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[] {
                new Client
                {
                    ClientId = "authserver",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                     AllowedScopes = new [] { "authserver" }
                },
                  new Client
                {
                    ClientId = "Auth_Implicit",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "authserver"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new [] { "http://localhost:3068" },
                  //  PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },
                },
                //new Client
                //{
                //    ClientId = "Auth_Implicit",
                //    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.Implicit,

                //     AllowedScopes = new [] {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "authserver"

                //    },
                //     AllowAccessTokensViaBrowser=true,
                //   AllowedCorsOrigins = { "http://132.186.195.49:4200", "https://132.186.195.49:8080", "http://localhost:4200",
                //        "https://localhost:4200","https://132.186.195.70:5000","http://132.186.195.70:5000" },

                //    //RedirectUris=new[]{ "http://132.186.195.70:3068/signin-oidc" }
                //    RedirectUris=new[]{ "http://localhost:3068/signin-oidc" },
                //     PostLogoutRedirectUris={ "http://localhost:3068/signout-callback-oidc" }
                //    //PostLogoutRedirectUris={ "http://l132.186.195.70:3068/signout-callback-oidc" }
                //},
                 new Client
                {
                    ClientId = "Auth_code",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,

                     AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "authserver"

                    },
                     AllowOfflineAccess=true,
                     AllowAccessTokensViaBrowser=true,
                    RedirectUris=new[]{ "http://localhost:3068/signin-oidc" },
                    PostLogoutRedirectUris={ "http://localhost:3068/signout-callback-oidc"}
                }

            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[] {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "kshitij",
                    Password = "password",
                    Claims=new[]{ new Claim("email","kshitij@gmail.com") }
                },
                 new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "admin",
                     Claims=new[]{ new Claim("email","kshitij@gmail.com") }
                }
            };
        }
    }
}
