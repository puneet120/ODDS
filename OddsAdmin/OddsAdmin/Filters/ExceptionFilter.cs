
using Odds.Core.Entity;
using Odds.Services.Interfaces;
using System.Web.Mvc;
using System;
using Odds.Core.ExceptionLog;

namespace OddsAdmin.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        /// <summary>
        /// Track Exceptions raised in the application
        /// </summary>
        /// <param name="filterContext">Get Detail for exception</param>
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");           
            Logger.LogFileWrite(ex.Message, ex.StackTrace);
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}