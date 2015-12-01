namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Platform")]
    public partial class Platform
    {
        public Platform()
        {
            Files = new HashSet<File>();
        }

        [StringLength(10)]
        public string ID { get; set; }

        [Column("Platform")]
        public string Platform1 { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
