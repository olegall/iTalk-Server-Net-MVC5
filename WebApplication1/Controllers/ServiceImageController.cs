using System;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ServiceImageController : ImageController
    {
        [HttpGet]
        [ImageOutputCache(VaryByParam = "serviceId", Duration = CACHE_DURATION)]
        public ActionResult Index(long serviceId)
        {
            IGenericRepository<ServiceImage> rep = new Repositories().ServiceImages;
            ServiceImage image = rep.Get().SingleOrDefault(x => x.ServiceId == serviceId);
            Check(image);
            string ext = GetExtension(image.FileName);
            CheckExtension(ext);
            HttpContext.Cache.Add(serviceId.ToString(),
                                  image.Bytes,
                                  null,
                                  Cache.NoAbsoluteExpiration,
                                  TimeSpan.FromSeconds(CACHE_DURATION),
                                  CacheItemPriority.Normal,
                                  null);
            Response.BinaryWrite((byte[])HttpContext.Cache[serviceId.ToString()]);
            Response.ContentType = $"image/{ext}";
            return new EmptyResult();
        }
    }
}