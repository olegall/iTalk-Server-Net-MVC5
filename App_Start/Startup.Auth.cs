using WebApplication1.Misc.Auth;
//using Cryotop.Models;
//using Cryotop.Models.Entities;
//using Cryotop.Providers;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using static Utils.RouteCollectionExtensions;
using WebApplication1.Providers;

namespace WebApplication1
{
    public partial class Startup
    {
        public void ConfigAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(DataContext.Create);
            app.CreatePerOwinContext<ClientManager>(ClientManager.Create);
            app.CreatePerOwinContext<ConsultantManager>(ConsultantManager.Create);

            // TODO: refresh token http://bitoftech.net/2014/07/16/enable-oauth-refresh-tokens-angularjs-app-using-asp-net-web-api-2-owin/
            var publicClientId = "self";

            var oAuthOptionsApi = new OAuthAuthorizationServerOptions
            {
                AuthenticationType = AuthType.GetAuthType(RouteType.Api),
                TokenEndpointPath = new PathString("/api/AuthFirebase"),
                Provider = new ApplicationOAuthProviderApi(publicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365),
                AllowInsecureHttp = true,
            };
            app.UseOAuthBearerTokens(oAuthOptionsApi);
        }
    }
}