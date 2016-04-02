using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
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
            A aobject = new A();
            aobject.Test();
        
            B bobk2 = new B();
            bobk2.Test();
        
            // from the service - a ratingsInformation object will be sent to the user:

            RatingInformation RI = new RatingInformation();
            RI.Ratings.Add(new RatingSections("Usability", 0, 5));
            RI.Ratings.Add(new RatingSections("Reliability", 0, 5));
            RI.Ratings.Add(new RatingSections("Activity", 0, 5));
            RI.Ratings.Add(new RatingSections("Availability", 0, 5));
            bool Running = true;
            bool Send = false;
            System.ConsoleKeyInfo menuup;
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Open GI Feed Back");
                Console.WriteLine(selected.ToString());
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
                        break;
                }




                
            }
            while (Running);
            







        }
    }
}
