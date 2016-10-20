using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Open.GI.hypermart.DAL;
using Open.GI.hypermart.Models;
using Open.GI.hypermart.DataTransformationObjects;

namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    ///  REST API layer for interacting with files.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class RatingsController : ApiController
    {
        private HypermartContext db = new HypermartContext();

        /// <summary>
        /// Gets the rating areas.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetAvailableRatingAreas()
        {
            return System.Configuration.ConfigurationManager.AppSettings["RatingAreas"].Split('|').ToList();
            
        }



        // GET: api/Ratings        
        /// <summary>
        /// Gets the ratings.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/Ratings/5        
        /// <summary>
        /// Gets the rating.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(string id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5        
        /// <summary>
        /// Puts the rating.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rating">The rating.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(string id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.userID)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings        
        /// <summary>
        /// Posts the rating.
        /// </summary>
        /// <param name="RatingToAdd">The rating.</param>
        /// <returns></returns>
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(RatingInformationDTO RatingToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = RequestContext.Principal;

            //try
            //{


            foreach (RatingDTO rating in RatingToAdd.Ratings)
            {
                Models.Rating newRating = new Models.Rating();
                newRating.ProductID = RatingToAdd.ProductID;
                newRating.userID = user.Identity.Name;
                newRating.RatingCategory = rating.RatedArea;
                newRating.rating = rating.Score;
                

                if (db.Ratings.Any(x => x.ProductID == newRating.ProductID && x.userID == newRating.userID && x.RatingCategory == newRating.RatingCategory))
                {
                    db.Ratings.Single(x => x.ProductID == newRating.ProductID && x.userID == newRating.userID && x.RatingCategory == newRating.RatingCategory).rating = newRating.rating;
                }
                else
                {
                    db.Ratings.Add(newRating);
                }
                db.SaveChanges();


            }




            //db.Ratings.Add(rating);

            //try
            //{
            //    db.SaveChanges();
           // }
            //catch (DbUpdateException)
           // {
            //    if (RatingExists(rating.userID))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
           // }

            return CreatedAtRoute("DefaultApi", new { id = RatingToAdd.ProductID  }, RatingToAdd);
        }

        // DELETE: api/Ratings/5        
        /// <summary>
        /// Deletes the rating.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(string id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(string id)
        {
            return db.Ratings.Count(e => e.userID == id) > 0;
        }
    }
}