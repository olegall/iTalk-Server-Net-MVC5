using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class ImageController : Controller
    {
        protected const int CACHE_DURATION = 86400;
        protected static readonly string[] VALID_EXTENSIONS = new string[4] { "png", "jpg", "gif", "jpeg" };

        protected void Check(Object image)
        {
            if (image == null)
            {
                throw new HttpException(404, null);
            }
        }

        protected string GetExtension(string name)
        {
            return name.Split('.').Last();
        }

        protected void CheckExtension(string ext)
        {
            if (!VALID_EXTENSIONS.Contains(ext.ToLower()))
            {
                throw new HttpException(404, null);
            }
        }
    }
}