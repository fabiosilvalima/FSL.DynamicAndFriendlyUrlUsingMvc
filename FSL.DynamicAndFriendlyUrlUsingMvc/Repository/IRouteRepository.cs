using FSL.DynamicAndFriendlyUrlUsingMvc.Handlers;
using System;
using System.Collections.Generic;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Repository
{
    public interface IRouteRepository : IDisposable
    {
        IEnumerable<RouteData> Find(string url);
    }
}
