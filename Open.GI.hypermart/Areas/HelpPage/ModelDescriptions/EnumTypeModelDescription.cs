using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Open.GI.hypermart.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Open.GI.hypermart.Areas.HelpPage.ModelDescriptions.ModelDescription" />
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTypeModelDescription"/> class.
        /// </summary>
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}