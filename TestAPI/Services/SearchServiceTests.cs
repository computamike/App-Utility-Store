using Moq;
using NUnit.Framework;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using Open.GI.hypermart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Services.Tests
{
    [TestFixture()]
    public class SearchServiceTests
    {
        [Test()]
        public void SearchServiceTest()
        {
            //Arrange
            var mock_db = new Mock<IHypermartContext>();
            var Product1 = new Product { ID = 1, Title = "First Product", Description = "First Product In Database" };
            var Product2 = new Product { ID = 2, Title = "Second Product", Description = "Second Product after the first product" };
            var Product3 = new Product { ID = 3, Title = "Third Product", Description = "Third Product." };



            var ProductsList = new List<Product>
            {
                Product1,
                Product2,
                Product3
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);

            var SUT = new SearchService(mock_db.Object);
            var SearchTerm = "First";

            //Act
            var result = SUT.PerformSearch(SearchTerm);
            //Assert
            mock_db.VerifyAll();
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(Product1));
            Assert.IsTrue(result.Contains(Product2));

        }

 
    }
}