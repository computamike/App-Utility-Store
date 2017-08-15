using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Open.GI.hypermart.Controllers;
using Moq;
using Open.GI.hypermart.DAL;
using System.Web.Mvc;
using Open.GI.hypermart.Models;
using System.Data.Entity;
using System.Net;

namespace TestAPI.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        [Test]
        public void testIndesReturnsAViewForTheRelevantProductID()
        {   //Arrange
            var mock_db = new Mock<IHypermartContext>();
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);
            ProductsController SUT = new ProductsController(mock_db.Object);
            
            //Act
            ActionResult  ViewResults =SUT.Index();

            //Assert
            ViewResult vResult = ViewResults as ViewResult;
            if (vResult != null)
            {
                Assert.AreEqual(string.Empty, vResult.ViewName);
            }
        }

        [TestCase(null, HttpStatusCode.BadRequest)]
        [TestCase(2, HttpStatusCode.NotFound)]

        public void testDetailsReturnsTheAppropriateErrorWhenNullIDPassed(int? productID, HttpStatusCode expected)
        {
            //Arrange
            var mock_db = new Mock<IHypermartContext>();
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);
            ProductsController SUT = new ProductsController(mock_db.Object);
            //Act
            var result = SUT.Details(productID);
            //Assert
            Assert.IsInstanceOf<HttpStatusCodeResult>(result);
            Assert.AreEqual(expected, (HttpStatusCode)(result as HttpStatusCodeResult).StatusCode);

        }
        
        [Test]
        public void testDetailsReturnsTheAppropriateRowWhenValidIDPassed()
        {
            //Arrange
            var mock_db = new Mock<IHypermartContext>();
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);
            ProductsController SUT = new ProductsController(mock_db.Object);
            //Act
            var result = SUT.Details(1);
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void testAddProduct()
        {
            //Arrange
            var fakeHTTPSession = new TitaniumBunker.PhonySession.FonySession();


            //prodcontroller.ControllerContext = fakeHTTPSession.BuildControllerContext(prodcontroller);
            //prodcontroller.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());


            var mock_db = new Mock<IHypermartContext>();
            var newProduct = new Product { Title = "SecondProduct", Description = "First Product In Database" };
            var ProductsList = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            };
            var mockProducts = TestAPI.GenerateDBSet.CreateMockDBSet<Product>(ProductsList);
            mock_db.Setup(x => x.Products).Returns(mockProducts.Object);
            mock_db.Setup(x => x.Products.Add(newProduct)).Returns(newProduct).Verifiable();

            ProductsController SUT = new ProductsController(mock_db.Object);
            SUT.ControllerContext = fakeHTTPSession.BuildControllerContext(SUT);
            SUT.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());
            //Act
            var result = SUT.Create(newProduct);

            //Assert
            Assert.AreEqual(1, ((RedirectToRouteResult)result).RouteValues.Keys.Count);
            Assert.AreEqual("action", ((RedirectToRouteResult)result).RouteValues.Keys.ToList()[0]);
            Assert.AreEqual(1, ((RedirectToRouteResult)result).RouteValues.Values.Count);
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);

        }


    }
}
