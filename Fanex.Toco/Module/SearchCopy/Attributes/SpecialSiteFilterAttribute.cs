namespace Fanex.CustomerSetting.Module.SearchCopy.Attributes
{
    using Core.Web;
    using Criteria;
    using Services;
    using System.Web.Mvc;

    public class SpecialSiteFilterAttribute : ActionFilterAttribute
    {
        private const string DeniedView = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var param = filterContext.ActionParameters;
            int custid = param.ContainsKey("custid") ? (int)param["custid"] : (param.ContainsKey("customerid") ? (int)param["customerid"] : 0);
            var username = param.ContainsKey("username") ? (param["username"] ?? string.Empty).ToString() : string.Empty;
            if (!(custid == 0 && string.IsNullOrEmpty(username)))
            {
                var permissionService = new PermissionService();
                var checkSpecialSite = permissionService.CheckSpecialSitePermission(new CheckSpecialSitePermissionCriteria()
                {
                    UserID = AppSession.AccId,
                    CustID = custid,
                    CustName = username
                });
                if (checkSpecialSite.ErrorCode > 0)
                {
                    filterContext.Result = new PartialViewResult
                    {
                        ViewName = DeniedView
                    };

                    (filterContext.Result as PartialViewResult).ViewBag.ErrorMessage = checkSpecialSite.ErrorMessage;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
