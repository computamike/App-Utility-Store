namespace Open.GI.hypermart.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Open.GI.hypermart.Models;

    /// <summary>
    /// Hypermart Context
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="Open.GI.hypermart.DAL.IHypermartContext" />
    public partial class HypermartContext : DbContext, IHypermartContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HypermartContext"/> class.
        /// </summary>
        public HypermartContext(): base("HypermartContext")
        {
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public virtual DbSet<File> Files { get; set; }
        /// <summary>
        /// Gets or sets the platforms.
        /// </summary>
        /// <value>
        /// The platforms.
        /// </value>
        public virtual DbSet<Platform> Platforms { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the screenshots.
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        public virtual DbSet<Screenshot> Screenshots { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasMany(e => e.Platforms)
                .WithMany(e => e.Files)
                .Map(m => m.ToTable("FilePlatform").MapLeftKey("Files_ID").MapRightKey("Platforms_ID"));

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Screenshots)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_ID);
        }


        public new void SaveChanges()
        {
            base.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
