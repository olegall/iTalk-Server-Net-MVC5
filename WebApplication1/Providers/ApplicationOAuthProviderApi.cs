using WebApplication1.Misc.Auth;
//using WebApplication1.Models.Entities;
//using WebApplication1.Models.Repositories;
using WebApplication1.Models;
using WebApplication1.DAL;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static Utils.RouteCollectionExtensions;

using Newtonsoft.Json;
using RestSharp;
using System.Web;
using System.Linq;

namespace WebApplication1.Providers
{
    public class ApplicationOAuthProviderApi : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        private readonly BLL.ConsultantManager consBLL = new BLL.ConsultantManager();

        public ApplicationOAuthProviderApi(string publicClientId)
        {
            _publicClientId = publicClientId ?? throw new ArgumentNullException("publicClientId");
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var manager = context.OwinContext.GetUserManager<ClientManager>();

            var data = await context.Request.ReadFormAsync();

            ClientRepo userRepo = new ClientRepo();

            const string apiUrl = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo";
            // Денис
            const string apiKey = "AIzaSyByk3Lr2_uH3nv51wrwz-qlXtFCvHYs9mk";
            // Юра 
            //const string apiKey = "AIzaSyCfp-bteOKAF1D9f0pYtTBV8r0jl5Fmx64";
            string token = HttpContext.Current.Request.Form["token"];
            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest(method: Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddQueryParameter("key", apiKey);
            request.AddBody(new { idToken = token });

            IRestResponse response = client.Execute(request);
            dynamic content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            string phone = (string)content?.users[0].phoneNumber;
            if (!String.IsNullOrEmpty(phone))
            {
                //Client client_ = context.UserName == null || data.Get("token") == null
                //? null
                //: await userRepo.FindByNameAsync(context.UserName);

                if (data.Get("token") == null)
                {
                    context.SetError("invalid_model");
                    return;
                }

                Client client_ = await userRepo.FindByPhoneAsync(phone);

                ClaimsIdentity oAuthIdentity = await client_.GenerateIdentityAsync(manager, AuthType.GetAuthType(RouteType.Api));
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, null);
                context.Validated(ticket);
            }
        }

        //private async void FindUser(string phone)
        //{
        //    Client client_ = await new ClientRepo().FindByPhoneAsync(phone);
        //    PrivateConsultant privateCons = consBLL.GetPrivateByPhone(phone);
        //    JuridicConsultant juridicCons = consBLL.GetJuridicByPhone(phone);
        //    ClaimsIdentity oAuthIdentity = null;
        //    if (privateCons != null)
        //    {
        //        oAuthIdentity = await client_.GenerateIdentityAsync(manager, AuthType.GetAuthType(RouteType.Api));
        //    }
        //}

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}