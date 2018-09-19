using System.Web.Mvc;

namespace WebApplication1.Utils
{
    public class ImageOutputCache : OutputCacheAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var ext = filterContext.HttpContext.Response.ContentType;
            base.OnResultExecuting(filterContext);
            filterContext.HttpContext.Response.ContentType = ext;
        }
    }
}