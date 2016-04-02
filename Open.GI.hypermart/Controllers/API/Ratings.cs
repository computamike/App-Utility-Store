using Open.GI.hypermart.DAL;
using Open.GI.hypermart.DataTransformationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    /// Ratings API Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class Ratings : ApiController
    {

        /// <summary>
        /// Gets the rating template.
        /// </summary>
        /// <returns></returns>
        public RatingInformationDTO GetRatingTemplate(int ProductID, int FileID)
        {
            RatingInformationDTO RatingInformation = new RatingInformationDTO();
            RatingInformation.ProductID = ProductID;
            RatingInformation.FileID = FileID;
            RatingInformation.Ratings.Add(new RatingDTO("Usability", 0, 5));
            RatingInformation.Ratings.Add(new RatingDTO("Reliability", 0, 5));
            RatingInformation.Ratings.Add(new RatingDTO("Activity", 0, 5));
            RatingInformation.Ratings.Add(new RatingDTO("Availability", 0, 5));
            return RatingInformation;
        }

        /// <summary>
        /// Adds the rating.
        /// </summary>
        /// <param name="RatingInformation">The rating information.</param>
        /// <param name="dbContext">The database context.</param>
        public void AddRating(RatingInformationDTO RatingInformation, IHypermartContext dbContext)
        {
            // Do stuff with ratings here.
          
        }




    }
}
