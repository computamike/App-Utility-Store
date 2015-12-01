namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Files = new HashSet<File>();
            ScreenShots = new HashSet<Screenshot>();
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Tagline { get; set; }

        [StringLength(50)]
        public string Lead { get; set; }

        public virtual ICollection<Screenshot> ScreenShots { get; set; }
        
        public virtual ICollection<File> Files { get; set; }
    }
}
