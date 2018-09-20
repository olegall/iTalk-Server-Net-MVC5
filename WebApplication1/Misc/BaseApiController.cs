using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Misc
{
    public class BaseApiController<TUserManager> : ApiController
    {
        protected const string USERNAME_EXISTS = "username_exists";

        //protected TUserManager UserManager => Request.GetOwinContext().GetUserManager<TUserManager>();
        //protected IAuthenticationManager AuthManager => Request.GetOwinContext().Authentication;

        protected long? UserId => User?.Identity?.GetUserId<long>();

        protected int GetUserTimezone()
        {
            var tz = Request.Headers.FirstOrDefault(x => x.Key == "Timezone");

            var successParse = int.TryParse(tz.Value?.FirstOrDefault(), out int result);

            return successParse ? result : 0;
        }
    }
}