using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace WebApplication1.Misc
{
    public class BaseController<TUserManager, TSignInManager> : Controller
    {
        protected TUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<TUserManager>();
        protected TSignInManager SignInManager => HttpContext.GetOwinContext().Get<TSignInManager>();
        protected IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected long? UserId => User?.Identity?.GetUserId<long>();

        //public int GetCookieTimezoneOffset(HttpCookieCollection cookies)
        //{
        //    return DateUtil.GetCookieTimezoneOffset(cookies, "Cryotop.TimeZone");
        //}

        //public int GetCookieTimezoneOffset()
        //{
        //    return DateUtil.GetCookieTimezoneOffset(Request.Cookies, "Cryotop.TimeZone");
        //}
    }
}