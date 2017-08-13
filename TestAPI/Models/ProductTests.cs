using NUnit.Framework;
using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Models.Tests
{
    [TestFixture()]
    public class ProductTests
    {
        [Test()]
        public void RatingAverageIsCorrect()
        {
            //Arrange
            var SUT = new Product();
            SUT.Ratings.Clear();
            SUT.Ratings.Add(new Rating() { RatingCategory = "foo", rating = 4 });
            SUT.Ratings.Add(new Rating() { RatingCategory = "foo", rating = 2 });

            //Act
            var averages = SUT.AverageRating;
            //Assert

            Assert.IsNotNull(averages.SingleOrDefault(x => x.RatingCategory == "foo"));

        }
    }
}