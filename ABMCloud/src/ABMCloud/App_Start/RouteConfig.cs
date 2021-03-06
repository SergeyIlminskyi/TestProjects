﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ABMCloud
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employee", action = "EmployeesList", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                 "Filter",
                 "{controller}/{action}"
            );
        }
    }
}