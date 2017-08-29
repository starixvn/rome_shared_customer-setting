// --------------------------------------------------------------------------
// <copyright file="KenoSettingController.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Services;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Keno controller.
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

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditSuper")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult EditSuper(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSuperSetting(customerInformationViewModel);
            return View(Constants.EditSuperCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateSuper")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateSuper(SuperSettingViewModel superSetting)
        {
            var results = _kenoSettingService.UpdateSuperCustomerSetting(superSetting);
            return Json(results);
        }

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditMaster")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult EditMaster(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetMasterSetting(customerInformationViewModel);
            return View(Constants.EditMasterCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateMaster")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")] // Used for ExtNAP & Alpha
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateMaster(MasterSettingViewModel masterSetting)
        {
            var results = _kenoSettingService.UpdateMasterCustomerSetting(masterSetting);
            return Json(results);
        }

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditAgent")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult EditAgent(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetAgentSetting(customerInformationViewModel);
            return View(Constants.EditAgentCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateAgent")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")] // Used for ExtNAP & Alpha
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateAgent(AgentSettingViewModel agentSetting)
        {
            var results = _kenoSettingService.UpdateAgentCustomerSetting(agentSetting);
            return Json(results);
        }

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SuperEditMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult SuperEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfSuperEditMember(customerInformationViewModel);
            return View(Constants.SuperEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateSuperOnMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")] // Used for ExtNAP & Alpha
        [AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateSuperOnMember(SuperEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfSuperEditMember(memberSetting);
            return Json(results);
        }

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "MasterEditMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult MasterEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfMasterEditMember(customerInformationViewModel);
            return View(Constants.MasterEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateMasterOnMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")] // Used for ExtNAP & Alpha
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateMasterOnMember(MasterEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfMasterEditMember(memberSetting);
            return Json(results);
        }

        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "AgentEditMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] // Used for ExtNAP & Alpha
        public ActionResult AgentEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfAgentEditMember(customerInformationViewModel);
            return View(Constants.AgentEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "UpdateAgentOnMember")] // Used for NAP
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")] // Used for ExtNAP & Alpha
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public JsonResult UpdateAgentOnMember(AgentEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfAgentEditMember(memberSetting);
            return Json(results);
        }

        private void SetTitleForMultipleEdit(CustomerInformationViewModel customerInformationViewModel)
        {
            ViewBag.Title = customerInformationViewModel.IsMultipleEdit == 1 ? Resources.LayoutTexts.EditMultipleKeno : Resources.LayoutTexts.UpdateKenoSetting;
        }
    }
}
