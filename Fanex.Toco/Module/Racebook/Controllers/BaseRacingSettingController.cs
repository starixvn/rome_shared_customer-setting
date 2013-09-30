// ************************************************************************************
// <copyright file="BaseRacingSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using App_Start;
    using Core.Security;
    using Core.Web;
    using Core.Web.Controllers;
    using Helper;
    using Newtonsoft.Json;
    using PTRacing.BusinessObjects;
    using Services;

    /// <summary>
    /// Class BaseRacingSettingController.
    /// </summary>
    public class BaseRacingSettingController : BaseController
    {
        private static IRacingSettingService _racingSettingService;

        public BaseRacingSettingController()
            : this(null)
        {
        }

        public BaseRacingSettingController(IRacingSettingService racingSettingService)
        {
            _racingSettingService = racingSettingService ?? new RacingSettingService();
        }

        /// <summary>
        /// Gets the customer information.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns>Return JSONRESULT.</returns>
        [HttpPost]
        public JsonResult GetCustomerInfo(RequestContext requestContext)
        {
            var result = _racingSettingService.GetCustomerInfo(requestContext);
            return Json(JsonConvert.SerializeObject(result, Formatting.Indented, SerializeHelper.GetJsonSerializerSetting()));
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
        /// Updates the customer settings.
        /// </summary>
        /// <param name="oldSettings">The old settings.</param>
        /// <param name="newSettings">The new settings.</param>
        /// <param name="currentContext">The current context.</param>
        /// <param name="otherSettings">The other settings.</param>
        /// <returns>Return JSON result.</returns>
        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public JsonResult UpdateCustomerSetting(Customer oldSettings, Customer newSettings, RequestContext currentContext, Dictionary<object, object> otherSettings)
        {
            IList<Result> updatedResults = new List<Result>();
            try
            {
                ICustomer oldSetting = oldSettings;
                ICustomer newSetting = newSettings;
                var hashOtherSettings = new Hashtable();
                if (otherSettings != null && otherSettings.Count > 0)
                {
                    hashOtherSettings.Add("isupdateallmembers", otherSettings["isupdateallmembers"]);
                    hashOtherSettings.Add("newmpositiontaking", otherSettings["newmpositiontaking"]);
                }

                var results = _racingSettingService.SaveCustomerSettings(oldSetting, newSetting, currentContext, hashOtherSettings);
                updatedResults = MessageHelper.ParseError(results.ErrorCode, results.ErrorMessage);
            }
            catch (Exception ex)
            {
                updatedResults.Add(new Result()
                {
                    ErrorCode = "100",
                    ErrorMessage = ex.Message
                });
            }

            return Json(updatedResults);
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
