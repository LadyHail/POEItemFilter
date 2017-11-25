using System.Web.Mvc;
using System.Web.Routing;

namespace POEItemFilter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Items",
                url: "UsersItems/{action}/{model}/{filterId}/{itemId}",
                defaults: new { controller = "UsersItems", action = "Index", model = UrlParameter.Optional, filterId = UrlParameter.Optional, itemId = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "RefreshDbItems",
                url: "UsersItems/Refresh/{baseType}/{type}/{attribute1}/{attribute2}",
                defaults: new { controller = "UsersItems", action = "Refresh", baseType = -1, type = -1, attribute1 = -1, attribute2 = -1 }
                );
        }
    }
}