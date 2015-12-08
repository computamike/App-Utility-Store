using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Helpers
{
    /// <summary>
    /// Platform helper Class.
    /// </summary>
    public static class PlatformHelper
    {
        /// <summary>
        /// Gets the platform string.
        /// </summary>
        /// <param name="PlatformShortCode">The platform short code.</param>
        /// <returns></returns>
        public static string  GetPlatformString(string PlatformShortCode)
        {
            var brand = Properties.Resources.ResourceManager.GetString(PlatformShortCode);

            if (brand == null)
            {

                return string.Format("<i class='fa fa-question-circle' data-toggle='tooltip' title='Cannot find details for platform : {0}'></i>",PlatformShortCode);
 

            }

                return brand;
        }


    }
}