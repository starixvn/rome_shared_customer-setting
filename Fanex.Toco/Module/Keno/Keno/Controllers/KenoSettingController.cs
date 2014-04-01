// --------------------------------------------------------------------------
// <copyright file="KenoSettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class KenoSettingController : KenoSettingBaseController
    {
        private static IKenoSettingService _kenoSettingService;

        public KenoSettingController()
            : this(null)
        {
        }

        public KenoSettingController(IKenoSettingService kenoSettingService)
        {
            _kenoSettingService = kenoSettingService ?? new KenoSettingService();
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditSuper")]
        public ActionResult EditSuper(int custId)
        {
            var customerSetting = _kenoSettingService.GetSuperSetting(custId);
            return View(Constants.EditSuperCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "UpdateSuper")]
        public JsonResult UpdateSuper(SuperSettingViewModel superSetting)
        {
            var results = _kenoSettingService.UpdateSuperCustomerSetting(superSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditMaster")]
        public ActionResult EditMaster(int custId)
        {
            var customerSetting = _kenoSettingService.GetMasterSetting(custId);
            return View(Constants.EditMasterCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateMaster")]
        public JsonResult UpdateMaster(MasterSettingViewModel masterSetting)
        {
            var results = _kenoSettingService.UpdateMasterCustomerSetting(masterSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditAgent")]
        public ActionResult EditAgent(int custId)
        {
            var customerSetting = _kenoSettingService.GetAgentSetting(custId);
            return View(Constants.EditAgentCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateAgent")]
        public JsonResult UpdateAgent(AgentSettingViewModel agentSetting)
        {
            var results = _kenoSettingService.UpdateAgentCustomerSetting(agentSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SuperEditMember")]
        public ActionResult SuperEditMember(int custId)
        {
            var customerSetting = _kenoSettingService.GetSettingInCaseOfSuperEditMember(custId);
            return View(Constants.SuperEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateSuperOnMember")]
        public JsonResult UpdateSuperOnMember(SuperEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfSuperEditMember(memberSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "MasterEditMember")]
        public ActionResult MasterEditMember(int custId)
        {
            var customerSetting = _kenoSettingService.GetSettingInCaseOfMasterEditMember(custId);
            return View(Constants.MasterEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateMasterOnMember")]
        public JsonResult UpdateMasterOnMember(MasterEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfMasterEditMember(memberSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "AgentEditMember")]
        public ActionResult AgentEditMember(int custId)
        {
            var customerSetting = _kenoSettingService.GetSettingInCaseOfAgentEditMember(custId);
            return View(Constants.AgentEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateAgentOnMember")]
        public JsonResult UpdateAgentOnMember(AgentEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfAgentEditMember(memberSetting);
            return Json(results);
        }
    }
}
