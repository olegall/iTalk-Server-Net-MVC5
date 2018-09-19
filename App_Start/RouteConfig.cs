using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ConsultantImage",
                url: "consimage/{consId}",
                defaults: new { controller = "ConsultantImage", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "Cryotop.Controllers" }
            );

            routes.MapRoute(
                name: "GalleryImage",
                url: "galleryimage/{consId}",
                defaults: new { controller = "GalleryImage", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "Cryotop.Controllers" }
            );

            routes.MapRoute(
                name: "CategoryImage",
                url: "categoryimage/{consId}",
                defaults: new { controller = "CategoryImage", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "Cryotop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
