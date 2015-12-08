using Open.GI.hypermart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.DataTransformationObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class FileDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDTO"/> class.
        /// </summary>
        public FileDTO()
        {
            Platforms = new HashSet<Platform>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the type of the storage.
        /// </summary>
        /// <value>
        /// The type of the storage.
        /// </value>
        public storageType StorageType { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the BLOB.
        /// </summary>
        /// <value>
        /// The BLOB.
        /// </value>
        public byte[] BLOB { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [StringLength(50)]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int? ProductID { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the platforms.
        /// </summary>
        /// <value>
        /// The platforms.
        /// </value>
        public virtual ICollection<Platform> Platforms { get; set; }
    }


 

}
