using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.Helpers
{
    public static class PlatformHelper
    {
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