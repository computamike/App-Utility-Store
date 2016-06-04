using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Open.GI.hypermart
{
    /// <summary>
    /// MVC - Route Registration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ADUser",
                url: "{controller}/{action}/{userid}",
                defaults: null,
                constraints: new { userid = @"[^0-9]+" }
                );

            routes.MapRoute(
                name: "Search",
                url: "Search/{SearchTerm}",
                defaults: new {  Controller = "Search",action="Index"}
                );

            routes.MapRoute(
                name: "API Default",
                url: "api/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
                );


        }
    }
}
