// --------------------------------------------------------------------------
// <copyright file="VirtualSportsBettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.9.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.BetSetting.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.BetSetting.Base;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTVirtualSports.Services;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VirtualSportsBettingController : BettingControllerBase
    {
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "VirtualSportsBetting")]
        public ActionResult Index(int id, int uplineId, int userId, string siteName)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            if (customer.RoleId != 4 || groups == null || !groups.Contains(customer.GroupId))
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml");
            }

            ViewBag.Username = customer.Username;

            var bettingLimitService = new CustomerService();
            var model = bettingLimitService.GetCustomer(new RequestContext
            {
                AdminId = userId,
                TargetCustId = id,
                CustId = 0,
                FormId = FormType.EditPT,
                IP = Request.UserHostAddress,
                Language = "En",
                LangKey = "E",
                RoleId = 5,
                TargetRoleId = customer.RoleId,
                SiteName = siteName,
                SubAccId = 0,
                UserName = string.Empty
            });

            return this.PartialView("~/Areas/CustomerSetting/Views/BetSetting/VirtualSports/Index.cshtml", model);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "VirtualSportsBetting")]
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

            return this.JsonBase(result, settings);
        }
    }
}
