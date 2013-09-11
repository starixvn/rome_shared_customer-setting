// --------------------------------------------------------------------------
// <copyright file="SportsbookBettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.8.2">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.BetSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.BetSetting.Base;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTEngine.Services;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// All Bet Settings
    /// </summary>
    public class SportsbookController : BettingControllerBase
    {
        #region Sportsbook Bet Setting

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SportsbookBetting")]
        public ActionResult Index(int id, int uplineId, int userId, string siteName)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            if (customer.RoleId != 4 || groups == null || !groups.Contains(customer.GroupId))
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml");
            }

            ViewBag.Username = customer.Username;

            var bettingLimitService = new BettingLimitService();

            var currentContext = new RequestContext()
            {
                FormId = FormType.EditPT,
                CustId = uplineId,
                TargetCustId = id,
                SubAccId = 0,
                AdminId = userId,
                SiteName = siteName,
                Language = "En",
                LangKey = "E",
            };

            var bettingLimit = bettingLimitService.GetBettingForEdit(currentContext);

            return this.PartialView("~/Areas/CustomerSetting/Views/BetSetting/Sportsbook/Index.cshtml", bettingLimit);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SportsbookBetting")]
        public ActionResult EditBetSettingPost(int id, int uplineId, int userId, string txtCustomerObject)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (customer.RoleId != 4 || groups == null || !groups.Contains(customer.GroupId))
            {
                return this.JsonBase(new { ErrorCode = 1, ErrorMessage = "You're not authorized on this function" }, settings);
            }

            var bettingLimit = JsonConvert.DeserializeObject<BettingLimit>(txtCustomerObject, settings);

            var requestContext = new RequestContext()
            {
                CustId = uplineId,
                TargetCustId = id,
                FormId = FormType.EditPT,
                SubAccId = 0,
                AdminId = userId,
                IP = Request.UserHostAddress,
                Language = "En"
            };

            var bettingLimitService = new BettingLimitService();

            var result = bettingLimitService.UpdateBetting(bettingLimit, requestContext);

            return this.JsonBase(new { ErrorCode = result.ErrorCode, ErrorMessage = result.ErrorMessage }, settings);
        }

        #endregion Sportsbook Bet Setting
    }
}