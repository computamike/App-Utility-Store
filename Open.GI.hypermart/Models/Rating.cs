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
    public class Rating
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
        public Category RatingCategory { get; set; }
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int rating { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

    }
}