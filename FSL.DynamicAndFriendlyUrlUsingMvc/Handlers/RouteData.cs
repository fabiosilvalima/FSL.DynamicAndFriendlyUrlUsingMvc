using System;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Handlers
{
    public sealed class RouteData
    {
        public RouteData()
        {
            Controller = "Home";
            Action = "Index";
        }

        public Guid RoouteId { get; internal set; }
        public string Url { get; internal set; }
        public string Controller { get; internal set; }
        public string Action { get; internal set; }
        public string BodyCss { get; internal set; }
        public long Id { get; set; }
    }
}