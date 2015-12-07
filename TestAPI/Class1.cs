using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using System;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using System.Web.Http;
using Owin;
using System.Net.Http;

namespace TestAPI
{
    [TestFixture]
    public class Class1
    {
        protected TestServer server;
        
        [TestFixtureSetUp]
        public void setup()
        {
            server = TestServer.Create(app =>
            {
                HttpConfiguration config = new HttpConfiguration();
               Open.GI.hypermart.WebApiConfig.Register(config);
                app.UseWebApi(config);
            });   
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            if (server != null)
                server.Dispose();
        }
        [Test]
        public void DoTestewW()
        {
            Assert.AreEqual(true ,true);
        }
        [Test]
        public async Task CanListApps()
        {
            HttpResponseMessage response = await server.CreateRequest("/api/api/GetAllProducts").GetAsync();

            var result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("\"Hello from foreign assembly!\"", result, "/api/values not configured correctly");

 
        }
    }
}
