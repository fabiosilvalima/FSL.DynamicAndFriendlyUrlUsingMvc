using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Handlers
{
    public sealed class UrlRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeData = requestContext.RouteData.Values;
            var url = routeData["urlRouteHandler"] as string;
            var route = UrlHandler.GetRoute(url);

            routeData["url"] = route.Url;
            routeData["controller"] = route.Controller;
            routeData["action"] = route.Action;
            routeData["id"] = route.Id;
            routeData["urlRouteHandler"] = route;

            return new MvcHandler(requestContext);
        }
    }
}