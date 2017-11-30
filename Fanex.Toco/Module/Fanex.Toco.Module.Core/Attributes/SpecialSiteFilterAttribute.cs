namespace Fanex.CustomerSetting.Module.Core.Attributes
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Common;
    using Criteria;
    using CustomerSetting.Module.Core.Web;
    using Services.Impl;

    public class SpecialSiteFilterAttribute : ActionFilterAttribute
    {
        private const string DeniedView = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml";

        private readonly string[] custIdParamName = { "custid", "customerid", "superid", "masterid", "agentid" };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var param = filterContext.ActionParameters;
            int? custid = GetCustIdParam(param);
            var username = GetUserNameParam(param);

            if (custid > 0 || !string.IsNullOrWhiteSpace(username))
            {
                var permissionService = new ServiceBase();

                var checkSpecialSite = permissionService.CheckSpecialSitePermission(new CheckSpecialSitePermissionCriteria()
                {
                    UserID = AppSession.CurrentSession.UserId,
                    CustID = custid,
                    CustName = username
                });

                if (checkSpecialSite.ErrorCode > 0)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = checkSpecialSite
                        };
                    }
                    else
                    {
                        filterContext.Result = new PartialViewResult
                        {
                            ViewName = DeniedView
                        };

                        (filterContext.Result as PartialViewResult).ViewBag.ErrorMessage = checkSpecialSite.ErrorMessage;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private int? GetCustIdParam(IDictionary<string, object> param)
        {
            List<int> list = new List<int>();

            foreach (var item in custIdParamName)
            {
                list.Add(param.ContainsKey(item) ? Converter.ToInt(param[item]) : 0);
            }

            int index = list.FindIndex(m => m > 0);

            return index >= 0 ? (int?)list[index] : null;
        }

        private string GetUserNameParam(IDictionary<string, object> param)
        {
            return param.ContainsKey("username") ? (param["username"] ?? string.Empty).ToString() : string.Empty;
        }
    }
}
