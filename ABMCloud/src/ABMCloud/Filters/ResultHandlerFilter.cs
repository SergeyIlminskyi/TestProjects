using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;

namespace ABMCloud.Filters
{
    public class ResultHandlerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var baseController = filterContext.Controller as BaseController;
            bool showSuccess = baseController.ShowSuccessMessage;
            bool isError = false;

            if (showSuccess && !isError)
            {
                baseController.Success("Operation completed successfully.");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}