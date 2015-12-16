using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Models
{
    /// <summary>
    /// HyperMart -  Tagging.  Tagging seems simple - it is after all a collection of strings that the user can choose to asociate with a file.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// The Tag to save or associate with a file - this might simplify to a Text collection (unsure)
        /// </summary>
        public string Name{ get; set; }
    }
}
