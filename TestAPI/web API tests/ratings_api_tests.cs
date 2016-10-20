using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Controllers.API;
using Open.GI.hypermart.Models;
using System.Data.Entity;

namespace TestAPI.web_API_tests
{
    [TestFixture]
    public class ratings_api_tests
    {
        [Test]
        public void Can_Get_All_Ratings_For_A_Product()
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
            //var apiRatings = new RatingsController();
            //apiRatings.GetRatings();
            //var RatingTemplate = apiRatings.GetRatings();
            //RatingTemplate.ProductID = 1;
            //RatingTemplate.FileID = 1;

            
            //apiRatings.AddRating(RatingTemplate, mockEFContext.Object);
        }
        
        [Test]
        public void Can_send_a_rating_for_a_Product_WEB_API()
        {
            var mockEFContext = new Mock<HypermartContext>();
            Database.SetInitializer<HypermartContext>(null);

            //var Ratings = new List<RatingDetails>
           // {
           //     new RatingDetails{ ProductID = 1, RatingCategory="RatingCategory", userID="User1", rating =5}
           // }.AsQueryable();

            //var mockRatingsSet = new Mock<DbSet<RatingDetails>>();
            //mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.Provider).Returns(Ratings.Provider);
            //mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.Expression).Returns(Ratings.Expression);
            //mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.ElementType).Returns(Ratings.ElementType);
            //mockRatingsSet.As<IQueryable<RatingDetails>>().Setup(m => m.GetEnumerator()).Returns(Ratings.GetEnumerator());

            //mockEFContext.Setup(x => x.RatingDetails).Returns(mockRatingsSet.Object);
            mockEFContext.Setup(x => x.SaveChanges()).Verifiable();

            //var apiRatings = new RatingDetailsController(mockEFContext.Object as HypermartContext);

            //RatingDetails RD = new RatingDetails();
            //RD.userID = "userID";
            //RD.RatingCategory = "RatingCategory";

            //apiRatings.PostRatingDetails(RD);
            mockEFContext.VerifyAll();
        }
        
     


    }
}
