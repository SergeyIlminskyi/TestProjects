using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMCloud.Filters
{
    public class ExceptionHandlerFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {

            var baseController = exceptionContext.Controller as BaseController;
            bool showSuccess = baseController.ShowSuccessMessage;

            
            baseController.Error("Operation completed not ! successfully.");
            
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is Exception)
            {
                //exceptionContext.Result = new ViewResult
                //{
                //    ViewName = "RangeErrorPage",
                //    ViewData = exceptionContext.Controller.ViewData
                //};
                //exceptionContext.ExceptionHandled = true;
            }
        }
    }
}