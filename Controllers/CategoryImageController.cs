using System;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryImageController : ImageController
    {
        [HttpGet]
        [ImageOutputCache(VaryByParam = "categoryId", Duration = CACHE_DURATION)]
        public ActionResult Index(long categoryId)
        {
            IGenericRepository<CategoryImage> rep = new Repositories().CategoryImages;
            CategoryImage image = rep.Get().SingleOrDefault(x => x.CategoryId == categoryId);
            Check(image);
            string ext = GetExtension(image.FileName);
            CheckExtension(ext);
            HttpContext.Cache.Add(categoryId.ToString(),
                                  image.Bytes,
                                  null,
                                  Cache.NoAbsoluteExpiration,
                                  TimeSpan.FromSeconds(CACHE_DURATION),
                                  CacheItemPriority.Normal,
                                  null);
            Response.BinaryWrite((byte[])HttpContext.Cache[categoryId.ToString()]);
            Response.ContentType = $"image/{ext}";
            return new EmptyResult();
        }
    }
}