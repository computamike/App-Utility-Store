using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Model object for storing the history of applications that a user has installed.
    /// </summary>
    public class InstallationHistory
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
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        [Key, Column(Order = 2)]
        public int FileID { get; set; }
        
        /// <summary>
        /// Date of installation
        /// </summary>
        public DateTime InstallationDate { get; set; }

    }
}