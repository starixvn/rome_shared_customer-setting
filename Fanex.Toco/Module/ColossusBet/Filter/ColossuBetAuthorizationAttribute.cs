namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Filter
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Configurations;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Services;
    using Resources;

    public class ColossuBetAuthorizationAttribute : AuthorizeAttribute
    {
        public bool IsHost { get; set; }

        public IColossusBetAuthorizationService authenticationService { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var authorizationType = Type.GetType(ColossusBetSection.Instance.AuthorizationServiceType);
            authenticationService = Activator.CreateInstance(authorizationType) as IColossusBetAuthorizationService;

            var isAuthorized = false;
            if (filterContext.ActionDescriptor.ActionName.ToLower().Contains("multiple"))
            {
                isAuthorized = IsHost ? IsAuthorizedHostMultiple(filterContext) : IsAuthorizedReadMultiple(filterContext);
            }
            else
            {
                isAuthorized = IsHost ? IsAuthorizedHost(filterContext) : IsAuthorizedRead(filterContext);
            }

            if (!isAuthorized)
            {
                UnAuthorizedAction(filterContext);
            }
        }

        private void UnAuthorizedAction(AuthorizationContext filterContext)
        {
            HttpResponseBase response = filterContext.HttpContext.Response;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                response.Clear();
                response.Write(Messages.UnauthorizeMsg);
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                response.End();
            }
            else
            {
                filterContext.Result = new RedirectResult("~/CustomerSetting/ColossusBetSetting/ColossusBetSetting/UnAuthorized");
            }
        }

        private string GetActionParameterValue(IValueProvider valueProvider, string parameterName)
        {
            return valueProvider.GetValue(parameterName) != null
                                ? valueProvider.GetValue(parameterName).AttemptedValue
                                : string.Empty;
        }

        private bool IsAuthorizedHost(AuthorizationContext filterContext)
        {
            return authenticationService.IsAuthorizedHost(filterContext);
        }

        private bool IsAuthorizedHostMultiple(AuthorizationContext filterContext)
        {
            var valueProvider = filterContext.Controller.ValueProvider;
            var usernames = GetActionParameterValue(valueProvider, "Usernames").Split(',').ToList();

            return authenticationService.IsAuthorizedHostMultiple(filterContext, usernames);
        }

        private bool IsAuthorizedRead(AuthorizationContext filterContext)
        {
            return authenticationService.IsAuthorizedRead(filterContext);
        }

        private bool IsAuthorizedReadMultiple(AuthorizationContext filterContext)
        {
            var requestForm = filterContext.HttpContext.Request.Form;
            var usernames = requestForm["usernames"];

            return authenticationService.IsAuthorizedReadMultiple(filterContext, usernames.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        }

    }
}