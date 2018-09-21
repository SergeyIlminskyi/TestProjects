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

            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is Exception)
            {
                //exceptionContext.Result =
                exceptionContext.ExceptionHandled = true;


            }


            //if (!exceptionContext.ExceptionHandled)
            //{
            //    string message = baseController._repository.RepositoryResponseMessage;
            //    _logger.Error(message);

            //    filterContext.Result = new ViewResult
            //    {
            //        ViewName = "RangeErrorPage",
            //        ViewData = filterContext.Controller.ViewData
            //    };
            //    filterContext.ExceptionHandled = true;
            //}
        }
    }
}