# FSL.DynamicAndFriendlyUrlUsingMvc

**Dynamic and Friendly URL using MVC**

Dynamic URL is a great feature working with MVC. Friendly urls are even better. The following approach i think is best way to work with friendly URL.

![enter image description here](https://fabiosilvalima.net/wp-content/uploads/2017/01/fabiosilvalima-url-dinamica-e-amigavel-no-mvc.jpg)

> **LIVE DEMO:**
> 
http://codefinal.com/FSL.DynamicAndFriendlyUrlUsingMvc/

> **FULL ARTICLE:**
>
> English: https://fabiosilvalima.net/en/dynamic-friendly-url-using-mvc/
>
> Português: https://fabiosilvalima.net/url-amigavel-e-dinamica-no-mvc/

---

What is in the source code?
---

#### <i class="icon-file"></i> FSL.DynamicAndFriendlyUrlUsingMvc

- Visual Studio solution file;
- MVC project;
- A class to handle route requests; 
- A route repository
- Controllers and views

> **Remarks:**

> - I created the application using the Web Application template. Visual Studio created a lot of files, views, scripts. 

---

What is the goal?
---

- The URLs must be stored in a Repository. It means I want to change and create new urls in my repository;
- One or more URL can be pointed to the same Controller/Action. It means I want to have alias for URLs;
- If an URL does not exists in my Repository, try to resolve using MVC Controller/Action default behavior. It means the MVC default behavior will still work;
- The URL can or not contain an ID at the end. It means that last segmment of those URLs can be a long ID number;

**Assumptions:**
- You need to create a virtual directory in your IIS.
- I will not use a database to store those URLs but I will use the repository pattern and dependency resolver to configure it. So, you can create a database repository in future.


Explaining...
---

First of all, MVC does not have a built in feature for dynamic and friendly URL. You must write your own custom code.

Class that identify a URL:

**Handlers/UrlHandler.cs**
```csharp
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
```

Route Handler that handles all requests:

**Handlers/UrlRouteHandler.cs**
```csharp
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
```

The route handler configuration:

**App_Start/RouteConfig.cs**
```csharp
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
```

Repository classes:

**Repository/IRouteRepository.cs**
```csharp
public interface IRouteRepository : IDisposable
    {
        IEnumerable<RouteData> Find(string url);
    }
```

**Repository/StaticRouteRepository.cs**
```csharp
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
```

I have created 2 URL. One Url will point to Help Controller and another for Articles Controller.

The dependency resolver configuration. I use Ninject to do that:

**App_Start/NinjectWebCommon.cs**
```csharp
private static void RegisterServices(IKernel kernel)
{
    kernel.Bind<Repository.IRouteRepository>().To<Repository.StaticRouteRepository>();
}
```

----------

References:
---

- ASP.NET MVC [click here][1];
- More at my blog [click here][2];

Licence:
---

- Licence MIT


---

![Programação no Mundo Real Design Patterns Vol. 1](https://www.fabiosilvalima.net/wp-content/uploads/2017/02/fabiosilvalima-ebook-design-patterns-INSTAGRAM-2.png)

  [1]: https://www.asp.net/mvc
  [2]: http://www.fabiosilvalima.com.br
