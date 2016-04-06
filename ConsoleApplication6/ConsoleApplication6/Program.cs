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
            API_Proxy api = new API_Proxy("http://localhost.fiddler:12672/api/StoreContent");
            RatingInformationDTO  RI = api.GetRatingsInformation();
            
            // Set up application
            RI.ProductID = 1;
            bool Running = true;
            
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
                        
                        Running = false;

                        api.sd(RI);
                        break;
                }




                
            }
            while (Running);
            







        }
    }


}
