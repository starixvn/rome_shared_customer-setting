// ************************************************************************************
// <copyright file="AgentController.cs" company="Nexcel Solutions Vietnam">
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
namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Newtonsoft.Json;

    /// <summary>
    /// Class AgentController.
    /// </summary>
    public class AgentController : BaseController
    {
        /// <summary>
        /// Edits the agent.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The upline identifier.</param>
        /// <returns>ActionResult object.</returns>
        public ActionResult EditAgent(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.AgentCasinoModel(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/EditAgent.cshtml", customerSetting);
        }

        /// <summary>
        /// Views the agent by super.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="uplineId">The upline identifier.</param>
        /// <returns>ActionResult object.</returns>
        public ActionResult ViewAgentBySuper(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.AgentCasinoModelBySuper(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/ViewAgentBySuper.cshtml", customerSetting);
        }

        /// <summary>
        /// Updates the agent post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="custName">Customer name.</param>
        /// <param name="uplineId">The upline identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="settingModel">The setting model.</param>
        /// <param name="customerModel">The customer model.</param>
        /// <returns>ActionResult object.</returns>
        public ActionResult UpdateAgentPost(int id, string custName, int uplineId, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateAgent(
                id, 
                custName,
                uplineId, 
                Request.UserHostAddress, 
                userId, 
                username, 
                BaseController.FromSystem, 
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel),
                JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}