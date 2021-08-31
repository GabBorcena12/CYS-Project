using System.Web;
using System.Web.Optimization;

namespace ChooseYourShoes
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/bootbox.js", 
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-dt.net/jquery.dataTables.js",
                      "~/Scripts/bootstrap-dt/dataTables.bootstrap.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
             

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/bootstrap-pulse.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css",
                      "~/Content/gb-customized.css"
                      , "~/Content/select2/dist/css/select2.min.css",
                      "~/Content/css/AdminLTE.min.css",
                      "~/Content/css/skins/_all-skins.min.css",
                      "~/Content/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/bootstrap-dt/dataTables.bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Styles/css").Include(
                      "~/Content/bootstrap/dist/css/bootstrap.css",
                      "~/Content/select2/dist/css/select2.min.css",
                      "~/Content/css/AdminLTE.min.css",
                      "~/Content/css/skins/_all-skins.min.css",
                      "~/Content/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      "~/Content/font-awesome/css/font-awesome.min.css"
                      ));


        }

    }
}
