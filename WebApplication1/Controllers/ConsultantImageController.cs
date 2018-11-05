using System;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ConsultantImageController : ImageController
    {
        [HttpGet]
        [ImageOutputCache(VaryByParam = "consId", Duration = CACHE_DURATION)]
        public ActionResult Index(long consId)
        {
            IGenericRepository<ConsultantImage> rep = Reps.ConsultantImages;
            ConsultantImage image = rep.Get().SingleOrDefault(x => x.ConsultantId == consId);
            Check(image);
            string ext = GetExtension(image.FileName);
            CheckExtension(ext);
            HttpContext.Cache.Add(consId.ToString(),
                                  image.Bytes,
                                  null,
                                  Cache.NoAbsoluteExpiration,
                                  TimeSpan.FromSeconds(CACHE_DURATION),
                                  CacheItemPriority.Normal,
                                  null);
            Response.BinaryWrite((byte[])HttpContext.Cache[consId.ToString()]);
            Response.ContentType = $"image/{ext}";
            return new EmptyResult();
        }
    }
}