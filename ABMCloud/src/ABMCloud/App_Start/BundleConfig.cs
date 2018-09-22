using System.Web.Optimization;

namespace ABMCloud
{ 
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/jquery-ui.min.css",
                "~/Content/Site.css"
            ));
        }
    }
}