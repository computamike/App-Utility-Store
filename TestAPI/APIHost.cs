using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Open.GI.hypermart.Controllers;
using System.Web;
using System.Web.Http;

using System.Web.Mvc;
using System.Web.Routing;

namespace TestAPI
{
    public class APIHost
    {

        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);



            appBuilder.UseWebApi(config); 
        }
    }
}
