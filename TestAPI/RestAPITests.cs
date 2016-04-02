using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Owin.Hosting;
using System.Net.Http;
namespace TestAPI
{
    /// <summary>
    /// Test that Services can be called with specific REST CALLS - this is mostly integration
    /// </summary>
    [TestFixture]
    public class RestAPITests
    {
        const string Address = "http://localhost:";
        const int port = 8086;
        IDisposable _webApp;
        private HttpClient client;
        [SetUp]
        public void Setup()
        {
            _webApp = WebApp.Start<APIHost>(Address + port);
            client = new HttpClient { BaseAddress = new Uri(Address + port) };
        }
        [TearDown]
        public void Teardown()
        {  
            _webApp.Dispose();
        }
     
        [Test]
        public void testRestCall()
        {
            var response = client.GetAsync("/api/Values").Result;
            var body = response.Content.ReadAsStringAsync().Result;
        }

        [Test]
        public void testRestCallSpecificCall()
        {
                var response = client.GetAsync("/api/Values/12").Result;
                var body = response.Content.ReadAsStringAsync().Result;

        }

        [Test]
        public void testRestCallSpecificGetByS()
        {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
                var response = client.GetAsync("/api/Values/GetByStatus?id=123").Result;
                var body = response.Content.ReadAsStringAsync().Result;
        }
    }
}
