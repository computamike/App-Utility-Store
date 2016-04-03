using Open.GI.hypermart.DAL;
using Open.GI.hypermart.DataTransformationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    /// Ratings Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class RatingsController : ApiController
    {
        private IHypermartContext db = new HypermartContext();

        /// <summary>
        /// Gets the ratings.
        /// </summary>
        /// <returns></returns>
        public RatingInformationDTO GetRatings()
        {
            RatingInformationDTO RI = new RatingInformationDTO();
            RI.Ratings.Add(new RatingDTO("Usability", 0, 5));
            RI.Ratings.Add(new RatingDTO("Reliability", 0, 5));
            RI.Ratings.Add(new RatingDTO("Activity", 0, 5));
            RI.Ratings.Add(new RatingDTO("Availability", 0, 5));
            return RI;
        }

        /// <summary>
        /// Create a New Product
        /// </summary>
        [HttpPost]
        public void PostRatings(RatingInformationDTO RatingToAdd)
        {
            try
            {
                // do stuff to store the rating here 
            }
            catch (Exception ex)
            {

                throw new Exception("Cannot add a rating", ex);
            }

        }
    
    
    }
}