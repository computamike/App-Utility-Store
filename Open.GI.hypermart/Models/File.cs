namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("File")]
    public partial class File
    {
        public File()
        {
            Platforms = new HashSet<Platform>();
        }

        public int ID { get; set; }

        public storageType  StorageType { get; set; }

        public string FileName { get; set; }

        public byte[] BLOB { get; set; }

        public string Link { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public int? ProductID { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
