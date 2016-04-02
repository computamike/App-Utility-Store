using Moq;
using NUnit.Framework;
using Open.GI.hypermart.Controllers;
using Open.GI.hypermart.Controllers.API;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TitaniumBunker.PhonySession;

namespace TestAPI
{
    [TestFixture]
    public class MOQTests
    {




        [Test]
        public void Can_send_a_rating_for_a_Product_WEB_API()
        {
            var mockEFContext = new Mock<IHypermartContext>();
            var Products = new List<Product> 
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());

            mockEFContext.Setup(x => x.Products).Returns(mockSet.Object);

            var apiRatings = new Ratings();
            var RatingTemplate = apiRatings.GetRatingTemplate(1, 1);

            RatingTemplate.ProductID = 1;
            RatingTemplate.FileID = 1;


            apiRatings.AddRating(RatingTemplate, mockEFContext.Object);

        }

        [Test]
        public void Can_Add_A_Product_Via_A_Controller()
        {
            var mockEFContext = new Mock<IHypermartContext>();
            var Products = new List<Product> 
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());

            mockEFContext.Setup(x => x.Products).Returns(mockSet.Object);

            var prodcontroller = new ProductsController() { db = mockEFContext.Object };

            var fakeHTTPSession = new TitaniumBunker.PhonySession.FonySession();
            fakeHTTPSession.AddFileUpload(new PhonyUploadFile("Screensjot.jpg", GetResourceAsStrream("TestAPI.img100.jpg"), "JPG"));
            prodcontroller.ControllerContext = fakeHTTPSession.BuildControllerContext(prodcontroller);
            prodcontroller.Url = new UrlHelper(fakeHTTPSession.BuildRequestContext());


            var res = prodcontroller.Create(new Product { ID = 2, Description = "foobar" });

        }

        private System.IO.Stream GetResourceAsStrream(string streamName)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName);
        }
    }
}
