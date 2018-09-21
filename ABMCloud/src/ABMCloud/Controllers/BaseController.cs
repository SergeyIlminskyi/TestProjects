using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMCloud.Controllers
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

        public void Error(string message, string resCode, string errormess)
        {
            TempData.Clear();
            if (!string.IsNullOrEmpty(resCode)) message += " : " + resCode + " - " + errormess;
            TempData.Add(Alerts.ERROR, message);
        }
    }
}