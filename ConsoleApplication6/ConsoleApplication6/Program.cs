using ClassLibrary1;
using Newtonsoft.Json;
using Open.GI.hypermart.DataTransformationObjects;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {

        private static string StarRatingString(int score, int Outof)
        {
            string res = new String('*', score) + new String('-', Outof-score);
            return res;

        }



        static void Main(string[] args)
        {
            // from the service - a ratingsInformation object will be sent to the user:
            APIReference api = new APIReference();
            RatingInformationDTO  RI = api.GetRatingsInformation();
            
            // Set up application
            RI.ProductID = 1;
            bool Running = true;
            bool Send = false;
            System.ConsoleKeyInfo menuup;
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Open GI Feed Back");

                int render =0;
                foreach (var item in RI.Ratings)
                { 
                    Console.ResetColor()  ;

                    if (render == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                  
                     
                     
                    
                    Console.WriteLine(item.RatedArea + StarRatingString(item.Score, item.OutOf));
                    render = render + 1;
                    Console.ResetColor();
                    

                }




                menuup = Console.ReadKey(true);
                switch (menuup.Key)
                {
                    case ConsoleKey.Escape:
                        Running = false;
                        //ShutdownRobot();
                        return;
                    case ConsoleKey.UpArrow:
                        if (selected> 0)
                        {
                            selected --;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (RI.Ratings[selected].Score < RI.Ratings[selected].OutOf)
                        {
                            RI.Ratings[selected].Score++;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (RI.Ratings[selected].Score > 0)
                        {
                            RI.Ratings[selected].Score--;
                        }
                        break;


                    case ConsoleKey.DownArrow:
                        if (selected < RI.Ratings.Count()-1)
                        {
                            selected++;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Send = true;
                        Running = false;

                        api.sd(RI);
                        break;
                }




                
            }
            while (Running);
            







        }
    }

    public class APIReference
    {
        public void sd(RatingInformationDTO Ratings)
        {
            var client = new RestClient("http://localhost:12672/api/Ratings");
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
            var client = new RestClient("http://localhost:12672/api/Ratings");
            client.Authenticator =  new NtlmAuthenticator();
            client.AddDefaultHeader("Host", "localhost:12672");
            client.AddDefaultHeader("Cache-Control", "max-age=0");
            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, sdch");
            client.AddDefaultHeader("Accept-Language", "en-GB,en;q=0.8,en-US;q=0.6");

            var request = new RestRequest("GetRatings", Method.GET);
            var response = client.Execute(request);

            var x = JsonConvert.DeserializeObject<RatingInformationDTO>(response.Content );
            
            return x;
        }
    }
}
