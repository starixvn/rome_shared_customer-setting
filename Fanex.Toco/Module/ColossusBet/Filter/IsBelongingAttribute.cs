namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Filter
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using Configurations;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Services;
    using Resources;

    public class IsBelongingAttribute : AuthorizeAttribute
    {
        public IColossusBetCheckBelongingService service { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (HasConfiguredColossusBet())
            {
                var type = Type.GetType(ColossusBetSection.Instance.AuthorizationServiceType);
                service = Activator.CreateInstance(type) as IColossusBetCheckBelongingService;

                var isBelonging = service.IsBelonging();
                if (!isBelonging)
                {
                    HandleAccessDeniedFunction(filterContext);
                }
            }

        }

        private bool HasConfiguredColossusBet()
            => ColossusBetSection.Instance != null && !string.IsNullOrEmpty(ColossusBetSection.Instance.AuthorizationServiceType);

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
                        errorMessage = Messages.DontHavePermission
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
                filterContext.Result = new RedirectResult("~/CustomerSetting/ColossusBetSetting/ColossusBetSetting/UnAuthorized");
            }
        }
    }
}