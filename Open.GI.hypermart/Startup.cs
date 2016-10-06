using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Open.GI.hypermart.Startup))]
namespace Open.GI.hypermart
{
    /// <summary>
    /// Class that will eventually form the kernel for the OAuth Authentication system for Scripts.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
