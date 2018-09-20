//using Cryotop.Models.Repositories;
using WebApplication1.DAL;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication1.Misc.Auth
{
    public class UserApiAuthorizeAttribute : AuthorizeAttribute
    {
        public UserApiAuthorizeAttribute()
        {
            //Roles = AuthRole.USER;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                this.HandleUnauthorizedRequest(actionContext);
                base.OnAuthorization(actionContext);
                return;
            }

            //var tokenClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimType.TOKEN);

            //if (tokenClaim == null)
            //{
            //    this.HandleUnauthorizedRequest(actionContext);
            //    base.OnAuthorization(actionContext);
            //    return;
            //}

            //ClientRepo repo = new ClientRepo();

            //var clientName = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.Name);

            //if (repo.FindByNameAsync(clientName.Value) == null)
            //{
            //    this.HandleUnauthorizedRequest(actionContext);
            //}

            base.OnAuthorization(actionContext);
        }
    }
}