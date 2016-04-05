using Newtonsoft.Json;
using Open.GI.hypermart.DataTransformationObjects;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
 
    public class API_Proxy
    {
        private string baseAddress;
        RestClient client;
        public API_Proxy(string API_Address)
        {
            baseAddress = API_Address;
            client = new RestClient(baseAddress);
        }
        public void sd(RatingInformationDTO Ratings)
        { 
            client.Authenticator = new NtlmAuthenticator();
            client.AddDefaultHeader("Host", "localhost:12672");
            client.AddDefaultHeader("Cache-Control", "max-age=0");
            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, sdch");
            client.AddDefaultHeader("Accept-Language", "en-GB,en;q=0.8,en-US;q=0.6");

            var request = new RestRequest("PostRatings", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Ratings);

            var response = client.Execute<RatingInformationDTO>(request);


            //var s = JsonConvert.SerializeObject(Ratings);
            //request.AddBody(s);
            //IRestResponse response = client.Execute(request);
            // RatingToAdd


        }
        public RatingInformationDTO GetRatingsInformation()
        {
            client.Authenticator = new NtlmAuthenticator();
            client.AddDefaultHeader("Host", "localhost:12672");
            client.AddDefaultHeader("Cache-Control", "max-age=0");
            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, sdch");
            client.AddDefaultHeader("Accept-Language", "en-GB,en;q=0.8,en-US;q=0.6");

            var request = new RestRequest("GetRatings", Method.GET);
            var response = client.Execute(request);

            var x = JsonConvert.DeserializeObject<RatingInformationDTO>(response.Content);

            return x;
        }
    }
}
