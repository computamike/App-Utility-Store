using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Model for storing rating information
    /// </summary>
    public class RatingDetails
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Key, Column(Order = 0)]
        public string userID { get; set; }
        /// <summary>
        /// Gets or sets the rating category.
        /// </summary>
        /// <value>
        /// The rating category.
        /// </value>
        [Key, Column(Order = 1)]
        public string RatingCategory { get; set; }
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [Key, Column(Order = 2)]
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the rated product.
        /// </summary>
        /// <value>
        /// The rated product.
        /// </value>
        [ForeignKey("ProductID")]
        public Product RatedProduct { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int rating { get; set; }

 

    }
}