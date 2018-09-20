using System.Web.Mvc;
using ABMCloud.Filters;

namespace ABMCloud
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorFilter());
        }
    }
}