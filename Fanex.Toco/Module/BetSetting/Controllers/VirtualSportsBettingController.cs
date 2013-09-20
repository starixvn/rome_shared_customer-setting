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
        /// <summary>
        /// Indexes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The up-line identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>ActionResult object.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "VirtualSportsBetting")]
        public ActionResult Index(int id, int uplineId, int userId, string siteName)
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            if (customer.RoleId != 4 || groups == null || !groups.Contains(customer.GroupId) || !siteName.Substring(3).ToUpper().Contains(customer.CurrencyName.ToUpper()))
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml");
            }

            ViewBag.Username = customer.Username;
            ViewBag.SyncCasino = customer.SyncCasino;
            ViewBag.SyncBingo = customer.SyncBingo;
            ViewBag.SyncLiveCasino = customer.SyncLiveCasino;

            var bettingLimitService = this.VirtualSportsService;
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

        /// <summary>
        /// Edits the bet setting post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The up-line identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="txtCustomerObject">The text customer object.</param>
        /// <returns>ActionResult object.</returns>
        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "VirtualSportsBetting")]
        public ActionResult EditBetSettingPost(int id, int uplineId, int userId, string txtCustomerObject, string siteName = "ACCUT")
        {
            var groups = GroupConfigurations.AllowBettingGroups;

            var customer = this.CustomerInfo(id, userId);

            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (customer.RoleId != 4 || groups == null || !groups.Contains(customer.GroupId) || !siteName.Substring(3).ToUpper().Contains(customer.CurrencyName.ToUpper()))
            {
                return this.JsonBase(new { ErrorCode = 1, ErrorMessage = "You're not authorized on this function" }, settings);
            }

            var newSetting = JsonConvert.DeserializeObject<Customer>(txtCustomerObject, settings);

            var requestContext = new RequestContext
            {
                CustId = uplineId, // upline id
                AgentId = 0, // unknow
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

            var bettingLimitService = this.VirtualSportsService;
            var oldSetting = bettingLimitService.GetCustomer(requestContext);
            var result = bettingLimitService.SaveCustomer(oldSetting, newSetting, requestContext, new Hashtable());

            return this.JsonBase(result, settings);
        }
    }
}
