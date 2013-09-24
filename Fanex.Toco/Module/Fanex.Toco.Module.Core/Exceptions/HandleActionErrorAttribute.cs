using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Extensions;
using Fanex.CustomerSetting.Module.Core.Models;

namespace Fanex.CustomerSetting.Module.Core.Exceptions
{
    public class HandleActionErrorAttribute : HandleErrorAttribute
    {
        //private readonly ILog _logger;
        
        public override void OnException(ExceptionContext filterContext)
        {
            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new Result{
                        ErrorCode = 500,
                        ErrorMessage = ResourceExtension.GetResource("Errors", "AjaxCallError")
                    }
                };
                
                
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            LogExtension.Log(filterContext.Exception);

        
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}