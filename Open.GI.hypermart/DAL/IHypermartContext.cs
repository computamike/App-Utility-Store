using System;
namespace Open.GI.hypermart.DAL
{
    /// <summary>
    /// Interface describing the functionality of the Database Context
    /// </summary>
    public interface IHypermartContext
    {
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.File> Files { get; set; }
        /// <summary>
        /// Gets or sets the platforms.
        /// </summary>
        /// <value>
        /// The platforms.
        /// </value>
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Platform> Platforms { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the screenshots.
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Screenshot> Screenshots { get; set; }
    }
}
