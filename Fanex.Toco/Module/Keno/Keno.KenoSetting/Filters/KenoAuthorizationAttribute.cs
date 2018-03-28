namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Configurations;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Services;
    using Fanex.CustomerSetting.Module.Keno.Resources;

    public class IsBelongingAttribute : AuthorizeAttribute
    {
        public IKenoCheckBelongingService service { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (HasConfiguredKeno())
            {
                var authorizationType = Type.GetType(KenoSection.Instance.AuthorizationServiceType);
                service = Activator.CreateInstance(authorizationType) as IKenoCheckBelongingService;

                var isBelonging = service.IsBelonging();
                if (!isBelonging)
                {
                    HandleAccessDeniedFunction(filterContext);
                }
            }
        }

        private bool HasConfiguredKeno()
            => KenoSection.Instance != null && !string.IsNullOrEmpty(KenoSection.Instance.AuthorizationServiceType);

        private void HandleAccessDeniedFunction(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        isSuccess = false,
                        errorCode = "1",
                        errorMessage = Messages.UnauthorizeMsg
                    },
                    ContentEncoding = Encoding.UTF8,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue,
                    RecursionLimit = int.MaxValue
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("~/CustomerSetting/KenoSetting/KenoSetting/UnAuthorized");
            }
        }
    }
}