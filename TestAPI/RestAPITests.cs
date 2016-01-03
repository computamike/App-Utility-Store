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
    [TestFixture]
    public class RestAPITests
    {
        [Test]
        public void testRestCall()
        {
            const int port = 8086;
            using (WebApp.Start<APIHost>("http://localhost:"+port))
            {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port )};
                var response = client.GetAsync("/api/Values").Result;
                var body = response.Content.ReadAsStringAsync().Result;
                
            }
        }

        [Test]
        public void testRestCallSpecificCall()
        {
            const int port = 8086;
            using (WebApp.Start<APIHost>("http://localhost:" + port))
            {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
                var response = client.GetAsync("/api/Values/12").Result;
                var body = response.Content.ReadAsStringAsync().Result;

            }
        }
        [Test]
        public void testRestCallSpecificGetByS()
        {
            const int port = 8086;
            using (WebApp.Start<APIHost>("http://localhost:" + port))
            {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
                var response = client.GetAsync("/api/Values/GetByStatus?id=123").Result;
                var body = response.Content.ReadAsStringAsync().Result;

            }
        }
    }
}
