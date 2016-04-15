// -----------------------------------------------------------------------
// <copyright file="AntiXSRFFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Security.Authenticate
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using Fanex.Module.Logs;
    using Fanex.Module.Logs.Services;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AntiXSRFFilter : ActionFilterAttribute
    {
        private const string AntiForgeryFormTokenName = "__RequestVerificationToken";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ValidateAntiXSRF())
            {
                ActionDenied(filterContext);
            }

            base.OnActionExecuting(filterContext);
        }

        private static void ActionDenied(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        errorCode = 1,
                        errorMessage = Resource.Errors.errorNotAuthorized,
                        isSuccess = false
                    }
                };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml"
                };
            }
        }

        private bool ValidateAntiXSRF()
        {
            try
            {
                var request = HttpContext.Current.Request;

                var formToken = request.Headers[AntiForgeryFormTokenName];
                if (string.IsNullOrWhiteSpace(formToken))
                {
                    return false;
                }

                var cookieValue = request.Cookies[AntiForgeryConfig.CookieName];
                if (cookieValue != null
                    && !string.IsNullOrWhiteSpace(cookieValue.Value))
                {
                    AntiForgery.Validate(cookieValue.Value, formToken);
                }
                else
                {
                    var exceptionLog = new Exception("Cannot read Request Verification Token from cookie.");
                    exceptionLog.Write(Formater.LogType.Warning);
                }
            }
            catch (Exception ex)
            {
                ex.Write(Formater.LogType.Warning);

                return false;
            }

            return true;
        }
    }
}
