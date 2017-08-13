using Moq;
using NUnit.Framework;
using Open.GI.hypermart.Controllers;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Open.GI.hypermart.Controllers.Tests
{
    [TestFixture()]
    public class SearchControllerTests
    {
        /// <exclude />
        [Test()]
        public void SearchesOnDescriptionAndTitle()
        {
            //Arrange
            var mock_db = new Mock<IHypermartContext>();
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "First Product",Description = "First Product In Database"},
                new Product{ID = 2,Title = "Second Product",Description = "Second Product after the first product"},
                new Product{ID = 3,Title = "Third Product",Description = "Third Product."},
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);

            var SUT = new SearchController(mock_db.Object);
            var SearchTerm = "First";
            
            //Act
            var result = SUT.Index(SearchTerm);
            //Assert
            mock_db.VerifyAll();
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}