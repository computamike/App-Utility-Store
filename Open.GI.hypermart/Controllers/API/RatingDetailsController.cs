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

namespace Open.GI.hypermart.Controllers.API
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class RatingDetailsController : ApiController
    {
        private HypermartContext db = new HypermartContext();
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingDetailsController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public RatingDetailsController(HypermartContext db)
        {
            this.db = db;
        }///
        public RatingDetailsController()
        {

        }
        // GET: api/RatingDetails        
        /// <summary>
        /// Gets the rating details.
        /// </summary>
        /// <returns></returns>
        public IQueryable<RatingDetails> GetRatingDetails()
        {
            return db.RatingDetails;
        }

        // GET: api/RatingDetails/5        
        /// <summary>
        /// Gets the rating details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(RatingDetails))]
        public IHttpActionResult GetRatingDetails(string id)
        {
            RatingDetails ratingDetails = db.RatingDetails.Find(id);
            if (ratingDetails == null)
            {
                return NotFound();
            }

            return Ok(ratingDetails);
        }

        // PUT: api/RatingDetails/5        
        /// <summary>
        /// Puts the rating details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ratingDetails">The rating details.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRatingDetails(string id, RatingDetails ratingDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ratingDetails.userID)
            {
                return BadRequest();
            }

            db.Entry(ratingDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingDetailsExists(id))
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

        // POST: api/RatingDetails        
        /// <summary>
        /// Posts the rating details.
        /// </summary>
        /// <param name="ratingDetails">The rating details.</param>
        /// <returns></returns>
        [ResponseType(typeof(RatingDetails))]
        public IHttpActionResult PostRatingDetails(RatingDetails ratingDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RatingDetails.Add(ratingDetails);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex )
            {
                Console.WriteLine(ex);
                if (RatingDetailsExists(ratingDetails.userID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ratingDetails.userID }, ratingDetails);
        }

        // DELETE: api/RatingDetails/5        
        /// <summary>
        /// Deletes the rating details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(RatingDetails))]
        public IHttpActionResult DeleteRatingDetails(string id)
        {
            RatingDetails ratingDetails = db.RatingDetails.Find(id);
            if (ratingDetails == null)
            {
                return NotFound();
            }

            db.RatingDetails.Remove(ratingDetails);
            db.SaveChanges();

            return Ok(ratingDetails);
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
        /// <summary>
        /// Ratings the details exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool RatingDetailsExists(string id)
        {
            return db.RatingDetails.Count(e => e.userID == id) > 0;
        }
    }
}