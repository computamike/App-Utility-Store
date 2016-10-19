using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Error page information
    /// </summary>
    public class HttpErrorPage
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode{ get; set; }
        /// <summary>
        /// Gets or sets the error title.
        /// </summary>
        /// <value>
        /// The error title.
        /// </value>
        public string ErrorTitle { get; set; }
        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string ErrorDescription { get; set; }
        /// <summary>
        /// Gets or sets the technical contact.
        /// </summary>
        /// <value>
        /// The technical contact.
        /// </value>
        public string TechnicalContact { get; set; }
    }
}
