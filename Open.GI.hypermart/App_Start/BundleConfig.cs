using System.Web;
using System.Web.Optimization;

namespace Open.GI.hypermart
{
    /// <summary>
    /// ASP.NET MVC Bundle configuration
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/profile.js",

                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Fyotx.css",
                      "~/Content/site.css"));

            // Adding support for DropZone
            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));
           
            var DZSB = new StyleBundle("~/bundles/dropzonecss");
            DZSB.Include("~/Scripts/dropzone/basic.css",
                     "~/Scripts/dropzone/dropzone.css");
            bundles.Add(DZSB);

            // Adding support for Star Rating...
            bundles.Add(new ScriptBundle("~/bundles/starrating").Include(
                "~/Scripts/bootstrap-star-rating/js/star-rating.js"));
            
            var SRSB = new StyleBundle("~/bundles/star-rating");
            SRSB.Include("~/Scripts/bootstrap-star-rating/css/star-rating.css");
            SRSB.Include("~/Scripts/bootstrap-star-rating/css/theme-krajee-svg.css");
            SRSB.Include("~/Scripts/bootstrap-star-rating/css/theme-krajee-fa.css");
            bundles.Add(SRSB);

            bundles.Add(new StyleBundle("~/Content/httpErrors").Include(
          "~/Content/HttpErrorPages/Layout.css"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
