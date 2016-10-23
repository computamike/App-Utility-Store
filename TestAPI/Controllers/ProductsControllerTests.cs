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
namespace TestAPI.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        [Test]
        public void testIndesReturnsAViewForTheRelevantProductID()
        {   
            var Products = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            }.AsQueryable();
            
            Mock<IHypermartContext> mockDBContext = new Mock<IHypermartContext>();
            
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());





            mockDBContext.Setup(x => x.Products).Returns(mockSet.Object);


            var xy = mockDBContext.Object.Products.ToList();

            ProductsController SUT = new ProductsController(mockDBContext.Object);
            ActionResult  ViewResults =SUT.Index();
            ViewResult vResult = ViewResults as ViewResult;
            if (vResult != null)
            {
                Assert.AreEqual(string.Empty, vResult.ViewName);
                //Assert.IsInstanceOfType(typeof(Product),vResult.Model.GetType() );
                //Product model = vResult.Model as Product;
                //if (model != null)
                //{
                //    //...
                //}
            }

        }

    }
}
