using Moq;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestAPI
{
    public static class GenerateDBSet
    {
       public static Mock<DbSet<T>> CreateMockDBSet<T>(List<T> FakeDBList ) where T : class 
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(FakeDBList.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(FakeDBList.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(FakeDBList.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(FakeDBList.AsQueryable().GetEnumerator());
            
            return mockSet; 
        }
         public static void testSyntax()
        {
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            };
            var res = CreateMockDBSet<Product>(ProductsList);
        }
 
    }
}
