namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Services;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Filters;

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

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [IsBelonging]
        public ActionResult EditSuper(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSuperSetting(customerInformationViewModel);
            return View(Constants.EditSuperCshtml, customerSetting);
        }

        [HttpPost]
        [IsBelonging]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [AntiXSRFFilter]
        public JsonResult UpdateSuper(SuperSettingViewModel superSetting)
        {
            var results = _kenoSettingService.UpdateSuperCustomerSetting(superSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")] 
        [IsBelonging]
        public ActionResult EditMaster(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetMasterSetting(customerInformationViewModel);
            return View(Constants.EditMasterCshtml, customerSetting);
        }

        [HttpPost]
        [IsBelonging]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")]
        [AntiXSRFFilter]
        public JsonResult UpdateMaster(MasterSettingViewModel masterSetting)
        {
            var results = _kenoSettingService.UpdateMasterCustomerSetting(masterSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [IsBelonging]
        public ActionResult EditAgent(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetAgentSetting(customerInformationViewModel);
            return View(Constants.EditAgentCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")]
        [AntiXSRFFilter]
        [IsBelonging]
        public JsonResult UpdateAgent(AgentSettingViewModel agentSetting)
        {
            var results = _kenoSettingService.UpdateAgentCustomerSetting(agentSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [IsBelonging]
        public ActionResult SuperEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfSuperEditMember(customerInformationViewModel);
            return View(Constants.SuperEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")]
        [AntiXSRFFilter]
        [IsBelonging]
        public JsonResult UpdateSuperOnMember(SuperEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfSuperEditMember(memberSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [IsBelonging]
        public ActionResult MasterEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfMasterEditMember(customerInformationViewModel);
            return View(Constants.MasterEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")]
        [AntiXSRFFilter]
        [IsBelonging]
        public JsonResult UpdateMasterOnMember(MasterEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfMasterEditMember(memberSetting);
            return Json(results);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "KenoSetting")]
        [IsBelonging]
        public ActionResult AgentEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            SetTitleForMultipleEdit(customerInformationViewModel);
            var customerSetting = _kenoSettingService.GetSettingInCaseOfAgentEditMember(customerInformationViewModel);
            return View(Constants.AgentEditMemberCshtml, customerSetting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "KenoSetting")]
        [AntiXSRFFilter]
        [IsBelonging]
        public JsonResult UpdateAgentOnMember(AgentEditMemberSettingViewModel memberSetting)
        {
            var results = _kenoSettingService.UpdateMemberCustomerSettingInCaseOfAgentEditMember(memberSetting);
            return Json(results);
        }

        public ActionResult UnAuthorized()
        {
            return View(KenoSection.Instance.UnAuthorizedPath);
        }

        private void SetTitleForMultipleEdit(CustomerInformationViewModel customerInformationViewModel)
        {
            ViewBag.Title = customerInformationViewModel.IsMultipleEdit == 1 ? Resources.LayoutTexts.EditMultipleKeno : Resources.LayoutTexts.UpdateKenoSetting;
        }
    }
}
