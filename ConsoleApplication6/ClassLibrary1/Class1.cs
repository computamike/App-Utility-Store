using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class RatingInformation
    {
        public RatingInformation()
        {
            Ratings = new List<RatingSections>();
        }
        public List<RatingSections> Ratings { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
    }


    public class A
    {
        public virtual void Test()
        {
            Console.WriteLine("A.Test");
        }
    }
    public class B: A
    { 
        
    }


 

}
