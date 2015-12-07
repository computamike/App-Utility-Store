using System;
namespace Open.GI.hypermart.DAL
{
    public interface IHypermartContext
    {
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.File> Files { get; set; }
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Platform> Platforms { get; set; }
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Product> Products { get; set; }
        System.Data.Entity.DbSet<Open.GI.hypermart.Models.Screenshot> Screenshots { get; set; }
    }
}
