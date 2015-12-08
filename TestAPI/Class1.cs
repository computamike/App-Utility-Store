using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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
        //[Test]
        //public async Task CanListApps()
        //{
            //HttpResponseMessage response = await server.CreateRequest("/api/StoreContent").GetAsync();

            //var result = await response.Content.ReadAsStringAsync();

            //Assert.AreEqual("\"Hello from foreign assembly!\"", result, "/api/values not configured correctly");

 
        //}
    }
}
