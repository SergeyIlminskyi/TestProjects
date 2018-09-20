using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMCloud.Filters
{
    public class HandleErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {

            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is Exception)
            {
                //exceptionContext.Result =
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}