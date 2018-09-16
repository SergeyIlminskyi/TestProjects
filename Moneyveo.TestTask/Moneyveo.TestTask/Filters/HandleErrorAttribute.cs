using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moneyveo.TestTask.Models;

namespace Moneyveo.TestTask
{
    public class HandleErrorAttribute : FilterAttribute, IExceptionFilter
    {

        private PartialViewResult result = new PartialViewResult()
        {
            ViewName = "../Shared/ErrorPartialView/Error",
            ViewData = new ViewDataDictionary()
        };

        public void OnException(ExceptionContext exceptionContext)
        {

            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is ValidationException)
            {
                result.ViewData.Model = new ErrorModel()
                {
                    Message = exceptionContext.Exception.Message,
                    Type = ErrorType.Warning
                };

                exceptionContext.Result = result;
                exceptionContext.ExceptionHandled = true;
            }

            if (!exceptionContext.ExceptionHandled)
            {
                result.ViewData.Model = new ErrorModel()
                {
                    Message = exceptionContext.Exception.Message,
                    Type = ErrorType.Critical
                };

                exceptionContext.Result = result;
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}