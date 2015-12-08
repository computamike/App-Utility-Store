namespace Open.GI.hypermart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Screen Shot Model Class
    /// </summary>
    public partial class Screenshot
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the screen shot1.
        /// </summary>
        /// <value>
        /// The screen shot1.
        /// </value>
        [Required]
        public byte[] ScreenShot1 { get; set; }

        /// <summary>
        /// Gets or sets the product_ identifier.
        /// </summary>
        /// <value>
        /// The product_ identifier.
        /// </value>
        public int? Product_ID { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public virtual Product Product { get; set; }
    }
}
