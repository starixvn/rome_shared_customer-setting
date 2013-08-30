// --------------------------------------------------------------------------
// <copyright file="RacingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.8.28">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.BetSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.BetSetting.Base;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.PTRacing.Services;
    using Fanex.PTRacing.BusinessObjects;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections;

    /// <summary>
    /// Racing controller for bet setting
    /// </summary>
    public class RacingController : BettingControllerBase
    {
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "RacingBetting")]
        public ActionResult Index(int id, int uplineId, int userId, string siteName)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            if (customer.RoleId != 4 || !groups.Contains(customer.GroupId))
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml");
            }

            ViewBag.Username = customer.Username;

            var bettingLimitService = new CustomerService();
            var model = bettingLimitService.GetCustomer(new RequestContext
            {
                CustId = uplineId,//upline id
                AgentId = 0,//unknow
                UserName = string.Empty,
                TargetCustId = id,
                AdminId = userId,
                SubAccId = 0,
                RoleId = 5,
                CurrencyId = 0,
                TargetRoleId = customer.RoleId,
                IsNeutralSite = false,
                FormId = FormType.EditPT,
                IP = HttpContext.Request.UserHostAddress
            });

            return this.View("~/Areas/CustomerSetting/Views/BetSetting/Racing/Index.cshtml", model);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "RacingBetting")]
        public ActionResult EditBetSettingPost(int id, int uplineId, int userId, string txtCustomerObject)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (customer.RoleId != 4 || !groups.Contains(customer.GroupId))
            {
                return this.JsonBase(new { ErrorCode = 1, ErrorMessage = "You're not authorized on this function" }, settings);
            }

            var newSetting = JsonConvert.DeserializeObject<Customer>(txtCustomerObject, settings);

            var requestContext = new RequestContext
            {
                CustId = uplineId,//upline id
                AgentId = 0,//unknow
                UserName = string.Empty,
                TargetCustId = id,
                AdminId = userId,
                SubAccId = 0,
                RoleId = 5,
                CurrencyId = 0,
                TargetRoleId = customer.RoleId,
                IsNeutralSite = false,
                FormId = FormType.EditPT,
                IP = HttpContext.Request.UserHostAddress
            };

            var bettingLimitService = new CustomerService();
            var oldSetting = bettingLimitService.GetCustomer(requestContext);
            var result = bettingLimitService.SaveCustomer(oldSetting, newSetting, requestContext, new Hashtable());

            return this.JsonBase(new { ErrorCode = result.ErrorCode, ErrorMessage = result.ErrorMessage }, settings);
        }
    }
}