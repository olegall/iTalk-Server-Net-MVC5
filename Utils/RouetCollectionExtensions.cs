using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace Utils
{
    public static class RouteCollectionExtensions
    {
        public static Route MapRouteWithName(
            this RouteCollection routes,
            string name,
            string url,
            object defaults = null,
            object constraints = null)
        {
            Route route = routes.MapRoute(name, url, defaults, constraints);
            var tokens = GetDataTokens(null, name, RouteType.Web);
            foreach (var item in tokens)
            {
                route.DataTokens.Add(item.Key, item.Value);
            }

            return route;
        }

        public static Route MapRouteWithName(
            this AreaRegistrationContext routes,
            string name,
            string url,
            object defaults = null,
            object constraints = null,
            string[] namespaces = null)
        {
            defaults = defaults ?? new { };
            constraints = constraints ?? new { };
            namespaces = namespaces ?? new string[] { };
            Route route = routes.MapRoute(name, url, defaults, constraints, namespaces);
            var tokens = GetDataTokens(routes.AreaName, name, RouteType.Web);
            foreach (var item in tokens)
            {
                route.DataTokens.Add(item.Key, item.Value);
            }

            return route;
        }

        public static IHttpRoute MapRouteWithName(
            this HttpRouteCollection routes,
            string name,
            string routeTemplate,
            RouteType routeType,
            IDictionary<string, object> defaults = null,
            IDictionary<string, object> constraints = null)
        {
            var dataTokens = GetDataTokens(null, name, routeType);
            var route = routes.CreateRoute(routeTemplate: routeTemplate, defaults: defaults, constraints: constraints, dataTokens: dataTokens);
            routes.Add(name, route);

            return route;
        }

        private static RouteValueDictionary GetDataTokens(string areaName, string routeName, RouteType routeType)
        {
            var tokens = new RouteValueDictionary();
            tokens.Add("RouteArea", areaName);
            tokens.Add("RouteName", routeName);
            tokens.Add("RouteType", routeType);

            return tokens;
        }

        public static RouteType GetRouteType(this HttpContext context)
        {
            return GetRouteType(GetDataTokens(context));
        }

        public static string GetRouteArea(this HttpContext context)
        {
            return GetRouteArea(GetDataTokens(context));
        }

        private static RouteType GetRouteType(RouteValueDictionary tokens)
        {
            return tokens != null && tokens.ContainsKey("RouteType") ? (RouteType)tokens["RouteType"] : RouteType.Unknown;
        }

        private static string GetRouteArea(RouteValueDictionary tokens)
        {
            return tokens != null && tokens.ContainsKey("RouteArea") ? (string)tokens["RouteArea"] : null;
        }

        private static RouteValueDictionary GetDataTokens(HttpContext context)
        {
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
            var tokens = routeData?.DataTokens;
            return tokens;
        }

        public enum RouteType
        {
            Unknown,
            Api,
            MedworkApi,
            Web
        }
    }
}