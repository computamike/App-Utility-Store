using Open.GI.hypermart.Models;
using System;
using System.Data.Entity;
namespace Open.GI.hypermart.DAL
{
    /// <summary>
    /// Interface describing the functionality of the Database Context
    /// </summary>
    public interface IHypermartContext :IDisposable  
    {
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.File> Files { get; set; }
        /// <summary>
        /// Gets or sets the platforms.
        /// </summary>
        /// <value>
        /// The platforms.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.Platform> Platforms { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the screenshots.
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.Screenshot> Screenshots { get; set; }
        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.Rating> Ratings { get; set; }

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        System.Data.Entity.IDbSet<Open.GI.hypermart.Models.RatingDetails> RatingDetails{ get; set; }




        /// <summary>
        /// In a DB Context object, this should save all changes made in this context to the underlying database
        /// </summary>
        void SaveChanges();
    }
}
