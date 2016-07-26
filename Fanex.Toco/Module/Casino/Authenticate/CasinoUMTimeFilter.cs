// -----------------------------------------------------------------------
// <copyright file="CasinoUMTimeFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Casino.Authenticate
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Configurations;
    using Fanex.CustomerSetting.Module.Casino.Resources;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CasinoUMTimeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CasinoSection.Instance.UMTime)
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
                        errorMessage = NGCasinoLang.errorNotAuthorized,
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
    }
}
