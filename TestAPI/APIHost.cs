using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Open.GI.hypermart.Controllers;
namespace TestAPI
{
    public class APIHost
    {
        
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller="API", id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
