using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ABMCloud.Models;

namespace ABMCloud.Helpers
{

    public static class UrlHelperExtension
    {

        private static string Filter(this UrlHelper helper, string route, PagingInfo filter)
        {
            return helper.RouteUrl(route,
                new RouteValueDictionary {
                                            {"FilterAction", "applyPage"},
                                            {"CurrentPagingInfo.Page", filter.Page},
                                            {"CurrentPagingInfo.ItemsPerPage", filter.ItemsPerPage},
                                         });
        }

        public static string Filter(this UrlHelper helper, PagingInfo filter)
        {
            return helper.Filter("Filter", filter);
        }

        public static string Filter(this UrlHelper helper, PagingInfo filter, int itemsPerPage)
        {
            filter.ItemsPerPage = itemsPerPage;
            return helper.Filter("Filter", filter);
        }
    }
}