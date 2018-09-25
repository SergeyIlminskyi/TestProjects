using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ABMCloud.Helpers;
using ABMCloud.Models;

namespace ABMCloud.HtmlHelpers
{
    public static class PagingHalpers
    {
        private const int PagerSize = 10;

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo filter, Func<PagingInfo, string> pageUrl)
        {
            var result = new StringBuilder();

            var currentFilter = new PagingInfo
            {
                Page = filter.Page,
                TotalItems = filter.TotalItems,
                ItemsPerPage = filter.ItemsPerPage,
            };

            var startPage = (filter.Page - 1) / PagerSize * 10 + 1;
            var lastPage = ((filter.Page - 1) / PagerSize + 1) * 10;

            result.Append("<ul class=\"pagination\">");

            if (currentFilter.Page > currentFilter.ItemsPerPage)
            {
                currentFilter.Page = startPage - currentFilter.ItemsPerPage;
                result.Append("<li class = \"page-item \"><a href=\"" + pageUrl(currentFilter) + "\" class = \"page-link\">...</a></li>");
            }

            if (filter.Page > 1)
            {
                currentFilter.Page = filter.Page - 1;
                result.Append("<li class = \"page-item \"><a href=\"" + pageUrl(currentFilter) + "\" class = \"page-link\">«</a></li>");
            }

            lastPage = lastPage > filter.TotalPages ? filter.TotalPages : lastPage;

            for (int i = startPage; i <= lastPage; i++)
            {
                currentFilter.Page = i;
                result.Append(i == filter.Page ? "<li class = \"page-link active\"><a class = \"page-link\">" + i + "</a></li>"
                    : "<li class = \"page-link \"><a href=\"" + pageUrl(currentFilter) + "\" class = \"page-link\">" + i + "</a></li>");
            }

            if (filter.Page < filter.TotalPages)
            {
                currentFilter.Page = filter.Page + 1;
                result.Append("<li class = \"page-item \"><a href=\"" + pageUrl(currentFilter) + "\" class = \"page-link\">»</a></li>");
            }

            if (lastPage < filter.TotalPages)
            {
                currentFilter.Page = lastPage + 1;
                result.Append("<li class = \"page-item \"><a href=\"" + pageUrl(currentFilter) + "\" class = \"page-link\">...</a></li>");
            }

            result.Append("</ul>");
            return MvcHtmlString.Create(result.ToString());
        }

        public static HtmlString ListCountPager(this HtmlHelper htmlHelper, int currentPageSize, long totalItemCount, int[] pageSizes, string actionName, RouteValueDictionary valuesDictionary)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new System.ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            var pager = new ListCountPager(htmlHelper.ViewContext, currentPageSize, 1, totalItemCount, pageSizes, valuesDictionary);
            return pager.RenderHtml();
        }
    }
}