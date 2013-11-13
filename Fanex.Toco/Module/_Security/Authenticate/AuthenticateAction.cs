using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Models;


namespace Fanex.CustomerSetting.Security.Authenticate
{
    public class AuthenticateAction : ActionFilterAttribute
    {
        private readonly AuthenticationType _authenticationType;
        private readonly string _mainAction;

        public enum AuthenticationType
        {
            Read,
            Host
        }

        public AuthenticateAction(AuthenticationType authenticationType, string functionName)
        {
            _authenticationType = authenticationType;
            _mainAction = functionName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionPath = controlName + "/" + _mainAction;
            var authenticateFactory = AuthenticateFactory.Create(_authenticationType, _mainAction);

            switch (_authenticationType)
            {
                case AuthenticationType.Read:                    
                    if (!authenticateFactory.AllowRead(actionPath))
                    {
                        ActionDenied(filterContext);
                        return;
                    }
                    break;
                case AuthenticationType.Host:
                    if (!authenticateFactory.AllowUpdate(actionPath))
                    {
                        ActionDenied(filterContext);
                        return;
                    }
                    break;
            }
            
            base.OnActionExecuting(filterContext);
        }

        private static void ActionDenied(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.Headers["X-Requested-With"] != null && request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new 
                    {
                        errorCode = 500,
                        errorMessage = Resource.Errors.errorNotAuthorized
                    }
                };
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml"
                };
            }
        }
    }
}
