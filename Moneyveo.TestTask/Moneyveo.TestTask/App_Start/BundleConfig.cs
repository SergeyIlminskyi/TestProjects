﻿using System.Web.Optimization;

namespace Moneyveo.TestTask
{ 
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"
            ));
        }
    }
}