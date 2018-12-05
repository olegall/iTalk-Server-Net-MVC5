using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using WebApplication1.Utils;

namespace WebApplication1.Misc
{
    public class BaseApiController<TUserManager> : ApiController
    {
        protected const string USERNAME_EXISTS = "username_exists";
        protected long? UserId => User?.Identity?.GetUserId<long>();

        protected int GetUserTimezone()
        {
            var tz = Request.Headers.FirstOrDefault(x => x.Key == "Timezone");

            var successParse = int.TryParse(tz.Value?.FirstOrDefault(), out int result);

            return successParse ? result : 0;
        }

        protected IHttpActionResult SendResult<TEntity>(CRUDResult<TEntity> result) where TEntity: class
        {
            if (result.Mistake == (int)CRUDResult<TEntity>.Mistakes.None)
                return Ok(true);

            return BadRequest(result.Mistake.ToString());
        }
    }
}