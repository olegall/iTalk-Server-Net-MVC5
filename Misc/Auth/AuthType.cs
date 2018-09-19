using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using static Utils.RouteCollectionExtensions;

namespace WebApplication1.Misc.Auth
{
    public static class AuthType
    {
        public static string GetAuthType(RouteType routeType, string routeArea = null)
        {
            string authType = null;
            switch (routeType)
            {
                case RouteType.Api:
                    authType = $"Cryotop.{OAuthDefaults.AuthenticationType}.Api";
                    break;

                case RouteType.MedworkApi:
                    authType = $"Cryotop.{OAuthDefaults.AuthenticationType}.MedworkApi";
                    break;

                case RouteType.Web:
                    authType = $"Cryotop.{DefaultAuthenticationTypes.ApplicationCookie}";
                    break;

                default:
                    authType = null;
                    break;

            }

            return authType;
        }
    }
}