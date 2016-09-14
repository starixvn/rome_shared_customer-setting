namespace Fanex.CustomerSetting.Module.NG2.Core.Attributes
{
    using Common;
    using Criteria;
    using CustomerSetting.Module.NG2.Core.Web;
    using Services.Impl;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SpecialSiteFilterAttribute : ActionFilterAttribute
    {
        private readonly string[] CustIdParamName = { "custid", "customerid", "superid", "masterid", "agentid" };

        private const string DeniedView = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var param = filterContext.ActionParameters;
            int? custid = this.GetCustIdParam(param);
            var username = GetUserNameParam(param);
            if (!(custid == 0 && string.IsNullOrEmpty(username)))
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
            list.Add(param.ContainsKey("custid") ? Converter.ToInt(param["custid"]) : 0);
            list.Add(param.ContainsKey("customerid") ? Converter.ToInt(param["customerid"]) : 0);
            list.Add(param.ContainsKey("superid") ? Converter.ToInt(param["superid"]) : 0);
            list.Add(param.ContainsKey("masterid") ? Converter.ToInt(param["masterid"]) : 0);
            list.Add(param.ContainsKey("agentid") ? Converter.ToInt(param["agentid"]) : 0);

            var index = list.FindIndex(m => m > 0);
            return index >= 0 ? (int?)list[index] : null;
        }

        private string GetUserNameParam (IDictionary<string, object> param)
        {
            string username = param.ContainsKey("username") ? (param["username"] ?? string.Empty).ToString() : string.Empty;
            return username;
        }
    }
}
