using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using Microsoft.Owin.Host.HttpListener;
using System.Net;
using ExtensionMethods;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.XmlDiffPatch;
using System.IO;
using System.Web.Script.Serialization;
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
            // This uber silly code is needed to ensure the Owin HttpListener assembly 
            // is properly copied to the output directory by using it, utterly redonkulous.
            //var uberSillyNecessity = typeof(OwinHttpListener);
            //if (uberSillyNecessity != null) { }

            //_webApp = WebApp.Start<APIHost>(Address + port);
            //client = new HttpClient { BaseAddress = new Uri(Address + port) };
        }
        [TearDown]
        public void Teardown()
        {  
            //_webApp.Dispose();
        }
     
        [Test]
        public void testRestCall()
        {
            using (WebApp.Start<APIHost>("http://localhost:" + port))
            {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
                var response = client.GetAsync("/api/Values").Result;
                var body = response.Content.ReadAsStringAsync().Result;
            }

        }

        // NOTE : Currently OWIN does not support MVC, therefore this test will not work under a self hosted OWIN system.
        //[Test]
        //public void testMVCHomeCall()
        //{
        //    const int port = 8087;
        //    using (WebApp.Start<APIHost>("http://localhost:" + port))
        //    {
        //        var client = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
        //        var response = client.GetAsync("/home").Result;
        //        var body = response.Content.ReadAsStringAsync().Result;
        //        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode,"NOT OK");
        //    }
        //}


        [Test]
        public void testRestCallSpecificCall()
        {
            string baseAddress = "http://localhost:8088/";

            // Start OWIN host 
            using (WebApp.Start<APIHost>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client2 = new HttpClient();

                var response2 = client2.GetAsync(baseAddress + "api/values").Result;

                Assert.AreEqual(HttpStatusCode.OK,response2.StatusCode,"Not OK" );


                var ERes = new string[] { "value1", "value2" };
                var strES = ERes.ToJSON();


                Assert.AreEqual(strES, response2.Content.ReadAsStringAsync().Result);
                Console.WriteLine(response2.Content.ReadAsStringAsync().Result);
            }
        }

        private bool CompareJson(string expected, string actual)
        {
            var expectedDoc = JsonConvert.DeserializeXmlNode(expected, "root");
            var actualDoc = JsonConvert.DeserializeXmlNode(actual, "root");
            var diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace |
                                   XmlDiffOptions.IgnoreChildOrder);
            using (var ms = new MemoryStream())
            {
                var writer = new XmlTextWriter(ms, Encoding.UTF8);
                var result = diff.Compare(expectedDoc, actualDoc, writer);
                if (!result)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Console.WriteLine(new StreamReader(ms).ReadToEnd());
                }
                return result;
            }
        }


        [Test]
        public void TestValuesController()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<APIHost>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
        [Test]
        public void testRestCallSpecificGetByS()
        {
            string baseAddress = "http://localhost:8088/";
            // Start OWIN host 
            using (WebApp.Start<APIHost>(url: baseAddress))
            {
                var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
                var response = client.GetAsync("/api/Values/GetByStatus?id=123").Result;
                var body = response.Content.ReadAsStringAsync().Result;
            
            }


        }
    }
}

namespace ExtensionMethods
{
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}