using System.Web;
using System.Web.Optimization;

namespace OddsAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/dataTables.bootstrap.js",
                       "~/Scripts/dataTables.responsive.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                       "~/Content/dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/toastrcss").Include(
                      "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/toastrjs").Include(
                     "~/Scripts/toastr.js"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
