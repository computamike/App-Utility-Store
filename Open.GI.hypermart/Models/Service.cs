using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// Class which decribes a Service, providing an OAuth Access token to allow secured access.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the name of the script
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the name of the user responsible for this script.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public User UserName { get; set; }
    }
}
