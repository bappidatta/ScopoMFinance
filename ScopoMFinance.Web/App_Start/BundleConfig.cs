using System.Web;
using System.Web.Optimization;

namespace ScopoMFinance.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/assets/vendors/tether/dist/js/tether.min.js",
                      "~/Content/assets/vendors/bootstrap/dist/js/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/basescript").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                      "~/Scripts/common-system-messages.js",
                      "~/Scripts/underscore.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/PagerSize.js",
                      "~/Scripts/pickadate/picker.js",
                      "~/Scripts/pickadate/picker.date.js",
                      "~/Scripts/pickadate/picker.time.js",
                      "~/Scripts/init.js"));

            bundles.Add(new StyleBundle("~/Content/basestyle").Include(
                      "~/Content/css/pickadate/classic.css",
                      "~/Content/css/pickadate/classic.date.css",
                      "~/Content/css/pickadate/classic.time.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/assets/common/css/source/main.css",
                      "~/Content/css/toastr.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Branch/Scripts/Organization/Index").Include(
                    "~/Areas/Branch/Scripts/Organization/Index.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/HO/Scripts/EmployeeType/Index").Include(
                    "~/Areas/HO/Scripts/EmployeeType/index.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/Common/Scripts/Employee/Index").Include(
                    "~/Areas/Common/Scripts/Employee/Index.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/Branch/Scripts/Organization/MapCreditOfficer").Include(
                    "~/Areas/Branch/Scripts/Organization/MapCreditOfficer.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/HO/Scripts/ComponentType/Index").Include(
                    "~/Areas/HO/Scripts/ComponentType/index.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/HO/Scripts/Component/Index").Include(
                    "~/Areas/HO/Scripts/Component/index.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/HO/Scripts/Component/MapBranch").Include(
                    "~/Areas/HO/Scripts/Component/map-branch.js"
              ));
        }
    }
}
