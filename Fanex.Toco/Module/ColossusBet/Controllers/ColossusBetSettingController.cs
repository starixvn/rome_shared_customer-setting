namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
using Fanex.CustomerSetting.Module.ColossusBetSetting.Services;

    /// <summary>
    /// Keno controller.
    /// </summary>
    public class ColossusBetSettingController : ColossusBetSettingBaseController
    {
        private static IColossusBetService _colossusBetService;

        public ColossusBetSettingController()
            : this(null)
        {
        }

        public ColossusBetSettingController(IColossusBetService colossusBetService)
        {
            _colossusBetService = colossusBetService ?? new ColossusBetService();
        }


        ////[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditSuper(int custId)
        {
            var setting = _colossusBetService.GetSuperSetting(custId);
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditMaster(int custId, int srecommend)
        {
            var setting = _colossusBetService.GetMasterSetting(custId, srecommend);
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditAgent(int custId, int mrecommend)
        {
            var setting = _colossusBetService.GetAgentSetting(custId, mrecommend);
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditMember(int custId, int recommend)
        {
            var setting = _colossusBetService.GetMemberSetting(custId, recommend);
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public JsonResult UpdateSetting(UpdatedSettingCriteria criteria)
        {
            var result = _colossusBetService.UpdateSetting(criteria);
            return Json(result);
        }

        private void SetTitleForMultipleEdit(ColossusBetModel customerInformationViewModel)
        {
            ////ViewBag.Title = customerInformationViewModel.IsMultipleEdit == 1 ? Resources.LayoutTexts.EditMultipleKeno : Resources.LayoutTexts.UpdateColossusBetSetting;
        }
    }
}
