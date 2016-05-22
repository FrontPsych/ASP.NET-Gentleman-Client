using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region SCRIPTS

            //bower
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/bower_components/jquery/dist/jquery.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js", // original ms package
                "~/Scripts/app/jquery.validate.globalize.datetime.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/cldr").Include(
                "~/bower_components/cldrjs/dist/cldr.js",
                "~/bower_components/cldrjs/dist/cldr/event.js",
                "~/bower_components/cldrjs/dist/cldr/supplemental.js",
                "~/bower_components/cldrjs/dist/cldr/unresolved.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
                "~/bower_components/globalize/dist/globalize.js",
                "~/bower_components/globalize/dist/globalize/number.js",
                "~/bower_components/globalize/dist/globalize/*.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/bower_components/moment/min/moment-with-locales.js"));
            //bower end


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
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryExtra").Include(
                      "~/Scripts/Theme/jquery.nav.js",
                      "~/Scripts/Theme/jquery.fitvids.js",
                      "~/Scripts/Theme/jquery.ajaxchimp.js",
                      "~/Scripts/Theme/jquery.scrollTo.js",
                      "~/Scripts/Theme/jquery.localScroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/Theme").Include(
                      "~/Scripts/Theme/preloader.js",
                      "~/Scripts/Theme/retina.js",
                      "~/Scripts/Theme/owl.carousel.js",
                      "~/Scripts/Theme/nivo-lightbox.js",
                      "~/Scripts/Theme/simple-expand.js",
                      "~/Scripts/Theme/custom.js"));

            #endregion

            #region STYLES

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/flags.css"));

            bundles.Add(new StyleBundle("~/Content/Theme").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Theme/assets/font-awesome/css/font-awesome.min.css",
                      "~/Content/Theme/css/colors/color.css",
                      "~/Content/Theme/css/preloader.css",
                      "~/Content/Theme/css/style.css",
                      "~/Content/Theme/css/responsive.css",
                        "~/Content/flags.css"));

            bundles.Add(new StyleBundle("~/Content/Theme/css").Include(
                      "~/Content/Theme/css/owl.theme.css",
                      "~/Content/Theme/css/owl.carousel.css",
                      "~/Content/Theme/css/nivo-lightbox.css",
                      "~/Content/Theme/css/themes/default/default.css"
                      ));


            #endregion
        }
    }
}
