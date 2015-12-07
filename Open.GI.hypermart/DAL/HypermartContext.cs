namespace Open.GI.hypermart.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Open.GI.hypermart.Models;

    public partial class HypermartContext : DbContext, Open.GI.hypermart.DAL.IHypermartContext
    {
        public HypermartContext(): base("HypermartContext")
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Screenshot> Screenshots { get; set; }

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
    }
}
