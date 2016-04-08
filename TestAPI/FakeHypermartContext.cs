using Open.GI.hypermart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Open.GI.hypermart.Models;
using FakeDbSet;
using System.Data.Entity;
namespace TestAPI
{
    /// <summary>
    /// Fake Hypermart DB COntext
    /// </summary>
    /// <seealso cref="Open.GI.hypermart.DAL.IHypermartContext" />
    public class FakeHypermartContext : DbContext ,IHypermartContext
    {

        public FakeHypermartContext()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            Files = new InMemoryDbSet<File> { FindFunction = (a, i) => a.FirstOrDefault(x => x.ID == i.Cast<int>().First()) };
            Platforms = new InMemoryDbSet<Platform>();//{ FindFunction = (a, i) => a.FirstOrDefault(x => x.ID == i.Cast<int>().First()) };
            Products = new InMemoryDbSet<Product> { FindFunction = (a, i) => a.FirstOrDefault(x => x.ID == i.Cast<int>().First()) };
            Screenshots = new InMemoryDbSet<Screenshot> { FindFunction = (a, i) => a.FirstOrDefault(x => x.ID == i.Cast<int>().First()) };
            Ratings = new InMemoryDbSet<Rating> ();
        }
        
        public IDbSet<File> Files { get; set; }

        public IDbSet<Platform> Platforms { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Screenshot> Screenshots { get; set; }

        public IDbSet<Rating > Ratings{ get; set; }

        public IDbSet<RatingDetails> RatingDetails { get; set; }

        void IHypermartContext.SaveChanges()
        {
            // Do nothing 
           // throw new NotImplementedException();
        }

        //public void Dispose()
        //{// do nothing
        //    throw new NotImplementedException();
        //}
    }
}
