using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Open.GI.hypermart.Controllers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestAPI
{
    public class APIHost
    {
        
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            
            RouteCollection routes = new RouteCollection();
            
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller="API", id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            foreach (RouteBase item in routes)
            {

                config.Routes.Add(System.Guid.NewGuid().ToString(), item);

                 
		 	}




            app.UseWebApi(config);
        }
    }
}
