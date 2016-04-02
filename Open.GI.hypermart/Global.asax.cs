using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Open.GI.hypermart
{
    /// <summary>
    /// Main MVC Application Class
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// ASP.NET MVC Application start method
        /// </summary>
        protected void Application_Start()
        {
            //Microsoft.Practices.Unity.UnityContainer container = new Microsoft.Practices.Unity.UnityContainer();
            //Registration.RegisterDependencies(container);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var s = System.Configuration.ConfigurationManager.AppSettings["ShareFolderName"]; 
            Helpers.FileShareHelper.CreateFolder (s);
             
              
            //Helpers.FileShareHelper.DeleteShare("ServerStore");
            //Helpers.FileShareHelper.CreateShare("Storage", "ServerStore", "Hypermart File Storage");

        }
    }
}
