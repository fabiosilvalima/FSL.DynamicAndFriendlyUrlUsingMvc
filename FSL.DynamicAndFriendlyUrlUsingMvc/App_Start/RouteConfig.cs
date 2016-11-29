using FSL.DynamicAndFriendlyUrlUsingMvc.Handlers;
using System.Web.Mvc;
using System.Web.Routing;

namespace FSL.DynamicAndFriendlyUrlUsingMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "IUrlRouteHandler",
                "{*urlRouteHandler}").RouteHandler = new UrlRouteHandler();
        }
    }
}
