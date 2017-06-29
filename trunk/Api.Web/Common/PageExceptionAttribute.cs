using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Api.Web.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class PageExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"]?.ToString();
            string actionName = filterContext.RouteData.Values["action"]?.ToString();
            HandleErrorInfo info = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.HttpContext.Response.Clear();
            HttpException hExp = filterContext.Exception as HttpException;
            if (hExp != null)
            {
                filterContext.HttpContext.Response.StatusCode = hExp.GetHttpCode();
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
            }
            filterContext.Result = new ViewResult() { ViewName = "/Views/Home/ErrorPage.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };
            filterContext.HttpContext.Response.SuppressContent = false;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            //base.OnException(filterContext);
        }
    }
}