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
        /// Gets my ratings.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="productID">The product identifier.</param>
        /// <returns></returns>
        public RatingInformationDTO GetMyRatings(string userID, int productID)
        {
            var y = db.RatingDetails.Where( x=> x.ProductID == productID && x.userID == userID);
            RatingInformationDTO RI = new RatingInformationDTO();
            foreach (Models.RatingDetails   item in y)
            {
                RI.Ratings.Add(new RatingDTO(item.RatingCategory,item.rating,5));
              
            }

            return RI;
        }


        /// <summary>
        /// Create a New Product
        /// </summary>
        [HttpPost]
        public void PostRatings(RatingInformationDTO RatingToAdd)
        {
            var user = RequestContext.Principal;

            //try
            //{
            
            
            foreach (RatingDTO rating in RatingToAdd.Ratings)
            {
                Models.RatingDetails newRating = new Models.RatingDetails();
                newRating.ProductID = RatingToAdd.ProductID;
                newRating.userID = user.Identity.Name;
                newRating.RatingCategory = rating.RatedArea;
                newRating.rating = rating.Score;


                if (db.RatingDetails.Any(x => x.ProductID == newRating.ProductID && x.userID == newRating.userID && x.RatingCategory == newRating.RatingCategory))
                {
                    db.RatingDetails.Single(x => x.ProductID == newRating.ProductID && x.userID == newRating.userID && x.RatingCategory == newRating.RatingCategory).rating = newRating.rating;
                }
                else
                {
                    db.RatingDetails.Add(newRating);
                }
                                
                db.SaveChanges();
            }
            
            // do stuff to store the rating here 

            //catch (Exception ex)
            //{
            //    throw new Exception("Cannot add a rating", ex);
            //}

        }
    }

}