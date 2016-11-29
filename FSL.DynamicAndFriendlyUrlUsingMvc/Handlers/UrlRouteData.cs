using System;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Handlers
{
    public sealed class UrlRouteData
    {
        public UrlRouteData()
            : base()
        {

        }

        public static implicit operator UrlRouteData(RouteData route)
        {
            var urlRoute = new UrlRouteData()
            {
                Action = route.Action,
                Controller = route.Controller,
                RoouteId = route.RoouteId,
                Url = route.Url,
                BodyCss = route.BodyCss,
                Id = route.Id
            };

            return urlRoute;
        }

        public Guid RoouteId { get; internal set; }
        public string Url { get; internal set; }
        public bool IsUrlPrefix { get; set; }
        public string Controller { get; internal set; }
        public string Action { get; internal set; }
        public string RequestedUrl { get; set; }
        public bool Success { get; internal set; }
        public string BodyCss { get; private set; }
        public long Id { get; set; }
    }
}