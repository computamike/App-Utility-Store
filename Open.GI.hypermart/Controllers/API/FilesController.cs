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
    /// REST API layer for interacting with files.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class FilesController : ApiController
    {
        private HypermartContext db = new HypermartContext();

        // GET: api/Files        
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns></returns>
        public IQueryable<File> GetFiles()
        {
            return db.Files;
        }

        // GET: api/Files/5        
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(File))]
        public IHttpActionResult GetFile(int id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // PUT: api/Files/5        
        /// <summary>
        /// Puts the file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFile(int id, File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != file.ID)
            {
                return BadRequest();
            }

            db.Entry(file).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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

        // POST: api/Files        
        /// <summary>
        /// Posts the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [ResponseType(typeof(File))]
        public IHttpActionResult PostFile(File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Files.Add(file);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = file.ID }, file);
        }

        // DELETE: api/Files/5        
        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(File))]
        public IHttpActionResult DeleteFile(int id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return NotFound();
            }

            db.Files.Remove(file);
            db.SaveChanges();

            return Ok(file);
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
        /// Files the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool FileExists(int id)
        {
            return db.Files.Count(e => e.ID == id) > 0;
        }
    }
}