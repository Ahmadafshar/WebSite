using System.Web.Mvc;
using System.Web.Routing;

namespace BaseCMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "Login",
                new {controller = "User", action = "Index"}
                );
            routes.MapRoute(
                "Logout",
                "Logout",
                new { controller = "User", action = "Logout" }
                );
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}