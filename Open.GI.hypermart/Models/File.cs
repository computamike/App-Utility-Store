namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
