// ************************************************************************************
// <copyright file="SportsbookController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

/// <summary>
/// The Controllers namespace.
/// </summary>
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
    /// Sports book Bet Settings.
    /// </summary>
    public class SportsbookController : BettingControllerBase
    {
        #region Sportsbook Bet Setting

        /// <summary>
        /// Indexes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The up-line identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>ActionResult object.</returns>
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

            var bettingLimitService = this.SportsBookService;

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

        /// <summary>
        /// Edits the bet setting post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The up-line identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="txtCustomerObject">The text customer object.</param>
        /// <returns>ActionResult object.</returns>
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

            var bettingLimitService = this.SportsBookService;

            var result = bettingLimitService.UpdateBetting(bettingLimit, requestContext);

            return this.JsonBase(new { ErrorCode = result.ErrorCode, ErrorMessage = result.ErrorMessage }, settings);
        }

        #endregion Sportsbook Bet Setting
    }
}