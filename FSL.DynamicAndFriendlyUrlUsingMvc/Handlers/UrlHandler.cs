using FSL.DynamicAndFriendlyUrlUsingMvc.Repository;
using System.Linq;
using System.Web.Mvc;
using System;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Handlers
{
    public sealed class UrlHandler
    {
        public static UrlRouteData GetRoute(string url)
        {
            url = url ?? "/";
            url = url == "/" ? "" : url;
            url = url.ToLower();

            UrlRouteData urlRoute = null;

            using (var repository = DependencyResolver.Current.GetService<IRouteRepository>())
            {
                var routes = repository.Find(url);
                var route = routes.FirstOrDefault();
                if (route != null)
                {
                    route.Id = GetIdFromUrl(url);
                    urlRoute = route;
                    urlRoute.Success = true;
                }
                else
                {
                    route = GetControllerActionFromUrl(url);
                    urlRoute = route;
                    urlRoute.Success = false;
                }
            }

            return urlRoute;
        }

        private static RouteData GetControllerActionFromUrl(string url)
        {
            var route = new RouteData();

            if (!string.IsNullOrEmpty(url))
            {
                var segmments = url.Split('/');
                if (segmments.Length >= 1)
                {
                    route.Id = GetIdFromUrl(url);
                    route.Controller = segmments[0];
                    route.Action = route.Id == 0? (segmments.Length >= 2? segmments[1] : route.Action) : route.Action;
                }
            }

            return route;
        }

        private static long GetIdFromUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var segmments = url.Split('/');
                if (segmments.Length >= 1)
                {
                    var lastSegment = segmments[segmments.Length - 1];
                    long id = 0;
                    long.TryParse(lastSegment, out id);

                    return id;
                }
            }

            return 0;
        }
    }
}