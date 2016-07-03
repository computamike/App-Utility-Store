using Moq;
using NUnit.Framework;
using Open.GI.hypermart.Controllers.API;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TitaniumBunker.PhonySession;

namespace TestAPI
{
    [TestFixture]
    public class MOQTests
    {

        [Test]
        public void xCan_send_a_rating_for_a_Product_WEB_API()
        {
            var mockEFContext = new Mock<HypermartContext>();
            Database.SetInitializer<HypermartContext>(null);
            var Products = new List<Product>
            {
                new Product{ID = 1,Title = "FirstProduct",Description = "First Product In Database"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(Products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(Products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(Products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(Products.GetEnumerator());

            var Ratings = new List<RatingDetails>
            {
                new RatingDetails{ ProductID = 1, RatingCategory="RatingCategory", userID="User1", rating =5}
            }.AsQueryable();

            var mockRatingsSet = new Mock<DbSet<RatingDetails>>();
            mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.Provider).Returns(Ratings.Provider);
            mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.Expression).Returns(Ratings.Expression);
            mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.ElementType).Returns(Ratings.ElementType);
            mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.GetEnumerator()).Returns(Ratings.GetEnumerator());

            //mockEFContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockEFContext.Setup(x => x.RatingDetails).Returns(mockRatingsSet.Object);

            mockEFContext.Setup(x => x.SaveChanges()).Verifiable();

            var apiRatings = new RatingDetailsController(mockEFContext.Object as HypermartContext);

            var allRatings = apiRatings.GetRatingDetails();

            RatingDetails RD = new RatingDetails();
            RD.userID = "userID";
            RD.RatingCategory = "RatingCategory";

            apiRatings.PostRatingDetails(RD);
            mockEFContext.VerifyAll();
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

            var prodcontroller = new Open.GI.hypermart.Controllers.ProductsController() { db = mockEFContext.Object };

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