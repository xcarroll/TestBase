using System.Web.Mvc;
using System.Web.Routing;

namespace TestBase
{
    public class TypicalMvcRouteConfig
    {
        /// <summary>
        /// Creates the typical route mappings supplied out of the box by Visual Studio MVC Project Wizards:
        /// <code>
        /// routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        /// routes.MapRoute(
        ///    name: "Default",
        ///     url: "{controller}/{action}/{id}",
        ///     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        ///     );
        /// </code>
        /// 
        /// The route used is <see cref="TypicalMvcDefaultRoute"/>
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(DefaultName, TypicalMvcDefaultRoute);
        }

        public const string DefaultName = "Default";

        /// <summary>
        /// The typical route supplied out of the box by Visual Studio MVC Project Wizards
        /// <code>
        ///     url: "{controller}/{action}/{id}",
        ///     defaults: new RouteValueDictionary(new {controller = "Home", action = "Index", id = UrlParameter.Optional}),
        ///     routeHandler: new MvcRouteHandler()
        /// </code>
        /// </summary>
        public static readonly Route TypicalMvcDefaultRoute =
            new Route(
                "{controller}/{action}/{id}",
                new RouteValueDictionary(new {controller = "Home", action = "Index", id = UrlParameter.Optional}),
                new MvcRouteHandler());
    }
}