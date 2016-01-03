using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.GI.hypermart.Attributes
{
    /// <summary>
    /// Custom attribute, assignable to an assembly that can store information connecting this .NET assembly to a Hypermarket Store entry.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Assembly)]
    public class StoreAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }
        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public int? FileID { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreAttribute"/> class.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="FileID">The file identifier.</param>
        public StoreAttribute(int ProductID, int FileID )
        {
            this.ProductID = ProductID;
            this.FileID = FileID;

        }
    }
}
