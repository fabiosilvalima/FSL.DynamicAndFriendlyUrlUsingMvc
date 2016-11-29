using System;
using System.Collections.Generic;
using FSL.DynamicAndFriendlyUrlUsingMvc.Handlers;
using System.Linq;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Repository
{
    public class StaticRouteRepository : IRouteRepository
    {
        public void Dispose()
        {

        }

        public IEnumerable<RouteData> Find(string url)
        {
            var routes = new List<RouteData>();
            routes.Add(new RouteData()
            {
                RoouteId = Guid.NewGuid(),
                Url = "how-to-write-file-using-csharp",
                Controller = "Articles",
                Action = "Index"
            });
            routes.Add(new RouteData()
            {
                RoouteId = Guid.NewGuid(),
                Url = "help/how-to-use-this-web-site",
                Controller = "Help",
                Action = "Index"
            });

            if (!string.IsNullOrEmpty(url))
            {
                var route = routes.SingleOrDefault(r => r.Url == url);
                if (route == null)
                {
                    route = routes.FirstOrDefault(r => url.Contains(r.Url)) ?? routes.FirstOrDefault(r => r.Url.Contains(url));
                }

                if (route != null)
                {
                    var newRoutes = new List<RouteData>();
                    newRoutes.Add(route);

                    return newRoutes;
                }
            }

            return new List<RouteData>();
        }
    }
}