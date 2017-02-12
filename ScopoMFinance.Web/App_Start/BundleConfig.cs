﻿using System.Web;
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/basescript").Include(
                      "~/Scripts/PagerSize.js",
                      "~/Scripts/pickadate/picker.js",
                      "~/Scripts/pickadate/picker.date.js",
                      "~/Scripts/pickadate/picker.time.js",
                      "~/Scripts/init.js"));

            bundles.Add(new StyleBundle("~/Content/basestyle").Include(
                      "~/Content/pickadate/classic.css",
                      "~/Content/pickadate/classic.date.css",
                      "~/Content/pickadate/classic.time.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
