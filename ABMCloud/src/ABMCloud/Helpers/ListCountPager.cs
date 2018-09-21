using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ABMCloud.Helpers
{
    public class ListCountPager
    {
        private ViewContext viewContext;

        private readonly int pageSize;

        private readonly int currentPage;

        private readonly long totalItemCount;

        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;

        private readonly int[] pageSizes;

        public ListCountPager(ViewContext viewContext, int pageSize, int currentPage, long totalItemCount, int[] pageSizes, RouteValueDictionary valuesDictionary)
        {
            this.viewContext = viewContext;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;
            this.pageSizes = pageSizes;
        }


        public HtmlString RenderHtml()
        {
            var sb = new StringBuilder();
            //sb.Append("<nav aria-label=\"List count navigation\"><ul class=\"pagination pagination-sm\">");

            foreach (var item in pageSizes)
            {
                if (pageSize == item)
                {
                    sb.AppendFormat("<a> {0}</a>", item);
                }
                else
                {
                    sb.Append(GenerateListCountPageLink(item.ToString(), item, currentPage));
                }
            }

            //sb.Append("</ul></nav>");

            return new HtmlString(sb.ToString());
        }

        private string GenerateListCountPageLink(string linkText, int pageSize, int currentPageNumber, int sortOrder = 0, bool sortDescending = false)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(linkWithoutPageValuesDictionary);

            //check if page is present in the link values or change it 
            if (!pageLinkValueDictionary.ContainsKey("CurrentPagingInfo.Page"))
            {
                pageLinkValueDictionary.Add("CurrentPagingInfo.Page", currentPageNumber);
            }
            else
            {
                pageLinkValueDictionary["CurrentPagingInfo.Page"] = currentPageNumber;
            }

            if (!pageLinkValueDictionary.ContainsKey("CurrentPagingInfo.ItemsPerPage"))
            {
                pageLinkValueDictionary.Add("CurrentPagingInfo.ItemsPerPage", pageSize);
            }
            else
            {
                pageLinkValueDictionary["CurrentPagingInfo.ItemsPerPage"] = pageSize;
            }

            if (!pageLinkValueDictionary.ContainsKey("CurrentPagingInfo.SortOrder"))
            {
                pageLinkValueDictionary.Add("CurrentPagingInfo.SortOrder", sortOrder);
            }
            else
            {
                pageLinkValueDictionary["CurrentPagingInfo.SortOrder"] = sortOrder;
            }

            if (!pageLinkValueDictionary.ContainsKey("CurrentPagingInfo.SortDescending"))
            {
                pageLinkValueDictionary.Add("CurrentPagingInfo.SortDescending", sortDescending);
            }
            else
            {
                pageLinkValueDictionary["CurrentPagingInfo.SortDescending"] = sortDescending;
            }

            // To be sure we get the right route, ensure the controller and action are specified.
            var routeDataValues = viewContext.RequestContext.RouteData.Values;
            if (!pageLinkValueDictionary.ContainsKey("controller") && routeDataValues.ContainsKey("controller"))
            {
                pageLinkValueDictionary.Add("controller", routeDataValues["controller"]);
            }
            if (!pageLinkValueDictionary.ContainsKey("action") && routeDataValues.ContainsKey("action"))
            {
                pageLinkValueDictionary.Add("action", routeDataValues["action"]);
            }
            else if (pageLinkValueDictionary.ContainsKey("action"))
            {
                pageLinkValueDictionary["action"] = routeDataValues["action"];
            }
            // 'Render' virtual path.
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLinkValueDictionary);

            if (virtualPathForArea == null)
                return null;

            var stringBuilder = new StringBuilder(" <a");

            stringBuilder.AppendFormat(" href=\"{0}\">{1}</a>", virtualPathForArea.VirtualPath, linkText);

            return stringBuilder.ToString();
        }
    }
}