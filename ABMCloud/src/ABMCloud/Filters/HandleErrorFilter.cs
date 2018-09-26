using System;
using System.Web.Mvc;

namespace ABMCloud.Filters
{
    public class ExceptionHandlerFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is Exception)
            {
                exceptionContext.Result = new ViewResult
                {
                    ViewName = "RangeErrorPage",
                    ViewData = exceptionContext.Controller.ViewData
                };
                exceptionContext.ExceptionHandled = true;
            }

            return;
        }
    }
}