using System.Web;
using System.Web.Mvc;
using ABMCloud.Models;
using ABMCloud.Helpers;
using ABMCloud.Dao;

namespace ABMCloud
{
    public static class Alerts
    {
        public const string SUCCESS = "success";
        public const string ATTENTION = "attention";
        public const string ERROR = "error";
        public const string INFORMATION = "info";

        public static string[] ALL
        {
            get { return new[] { SUCCESS, ATTENTION, INFORMATION, ERROR }; }
        }
    }

    public class BaseController : Controller
    {
        
        public bool ShowSuccessMessage { get; set; }


        public BaseController()
        {
        }

        public void Attention(string message)
        {
            TempData.Clear();
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Clear();
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Clear();
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Clear();
            TempData.Add(Alerts.ERROR, message);
        }

        protected TFilter ProcessFilter<TFilter>(TFilter filter) where TFilter : FilterModel, new()
        {

            var filterCode = typeof(TFilter).Name;

            if (filter.FilterAction != null)
            {
                switch (filter.FilterAction)
                {
                    case "applyFilter":
                        {
                            var cFilter = new TFilter();
                            filter.CurrentPagingInfo.Page = 1;
                            cFilter.CopyFrom(filter);
                            Session[filterCode] = cFilter;
                            break;
                        }
                    case "applyPage":
                        {
                            var fil = new TFilter();
                            fil.CopyFrom(filter);
                            if (Session[filterCode] != null)
                            {
                                fil = Session[filterCode] as TFilter; ;
                                if (fil != null) fil.CurrentPagingInfo = filter.CurrentPagingInfo;
                            }
                            Session[filterCode] = fil;
                            return fil;
                        }
                    case "resetFilter":
                        {
                            if (HttpContextFactory.Current.Session != null)
                                HttpContextFactory.Current.Session[filterCode] = null;
                            break;
                        }
                }
            }
            else
            {
                if (HttpContextFactory.Current.Session != null && HttpContextFactory.Current.Session[filterCode] != null)
                {
                    filter = HttpContextFactory.Current.Session[filterCode] as TFilter;
                }
            }

            return filter;
        }
    }
}