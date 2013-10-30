// ************************************************************************************
// <copyright file="BaseCloneController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Thai.Tran date="2013.10.30"">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.SearchCopy.App_Start;
    using Fanex.CustomerSetting.Module.SearchCopy.Helper;
    using Fanex.CustomerSetting.Module.SearchCopy.Services;
    using Fanex.PTRacing.BusinessObjects;
    using Newtonsoft.Json;

    /// <summary>
    /// Class BaseRacingSettingController.
    /// </summary>
    public class BaseCloneController : BaseController
    {
        private static ISearchCopyService _searchCopyService;

        public BaseCloneController()
            : this(null)
        {
        }

        public BaseCloneController(ISearchCopyService searchCopyService)
        {
            _searchCopyService = searchCopyService ?? new SearchCopyService();
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="uplineId">The UPLINE identifier.</param>
        /// <param name="targetCustId">The target customer identifier.</param>
        /// <param name="targetRoleId">The target role identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Return setting in JSON format.</returns>
        [HttpPost]
        public JsonResult GetSetting(int uplineId, int targetCustId, int targetRoleId, int roleId)
        {
            IRequestContext currentContext = new RequestContext()
            {
                CustId = uplineId, ////upline id
                AgentId = 0,
                UserName = string.Empty,
                TargetCustId = targetCustId,
                AdminId = AppSession.AccId,
                SubAccId = 0,
                RoleId = roleId,
                CurrencyId = 0,
                TargetRoleId = targetRoleId,
                IsNeutralSite = false,
                FormId = FormType.EditPT,
                IP = System.Web.HttpContext.Current.Request.UserHostAddress
            };

            return Json(JsonConvert.SerializeObject(currentContext, Formatting.Indented, SerializeHelper.GetJsonSerializerSetting()));
        }     
        
        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page.
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            //AppSession.AccId = 8;
            ResourceHelper.SetCulture();
            var rootUrl = Url.Content("~") + filterContext.RouteData.DataTokens["area"].ToString() + "/" + ModuleRegistrator.Module + "/";
            ViewBag.RootUrl = rootUrl;
            base.OnActionExecuting(filterContext);
        }
    }
}
