using Open.GI.hypermart.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Open.GI.hypermart
{
    /// <summary>
    /// Configuring Web API layer
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Filters.Add(new IdentityBasicAuthenticationAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue(@"text/html"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
