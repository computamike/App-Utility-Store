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
    ///  REST API layer for interacting with screenshots.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ScreenshotsController : ApiController
    {
        private HypermartContext db = new HypermartContext();

        // GET: api/Screenshots        
        /// <summary>
        /// Gets the screenshots.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Screenshot> GetScreenshots()
        {
            return db.Screenshots;
        }

        // GET: api/Screenshots/5        
        /// <summary>
        /// Gets the screenshot.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Screenshot))]
        public IHttpActionResult GetScreenshot(int id)
        {
            Screenshot screenshot = db.Screenshots.Find(id);
            if (screenshot == null)
            {
                return NotFound();
            }

            return Ok(screenshot);
        }

        // PUT: api/Screenshots/5        
        /// <summary>
        /// Puts the screenshot.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="screenshot">The screenshot.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScreenshot(int id, Screenshot screenshot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != screenshot.ID)
            {
                return BadRequest();
            }

            db.Entry(screenshot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenshotExists(id))
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

        // POST: api/Screenshots        
        /// <summary>
        /// Posts the screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot.</param>
        /// <returns></returns>
        [ResponseType(typeof(Screenshot))]
        public IHttpActionResult PostScreenshot(Screenshot screenshot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Screenshots.Add(screenshot);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = screenshot.ID }, screenshot);
        }

        // DELETE: api/Screenshots/5        
        /// <summary>
        /// Deletes the screenshot.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Screenshot))]
        public IHttpActionResult DeleteScreenshot(int id)
        {
            Screenshot screenshot = db.Screenshots.Find(id);
            if (screenshot == null)
            {
                return NotFound();
            }

            db.Screenshots.Remove(screenshot);
            db.SaveChanges();

            return Ok(screenshot);
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
        /// Screenshots the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool ScreenshotExists(int id)
        {
            return db.Screenshots.Count(e => e.ID == id) > 0;
        }
    }
}