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
            var setting = _colossusBetService.GetSetting(new GetSuperSettingCriteria()
            {
                CustId = custId
            }, custId, 4);

            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditMaster(int custId, int srecommend)
        {
            var setting = _colossusBetService.GetSetting(new GetMasterSettingCriteria()
            {
                CustId = custId,
                Srecommend = srecommend
            }, custId, 3);

            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditAgent(int custId, int mrecommend)
        {
            var setting = _colossusBetService.GetSetting(new GetAgentSettingCriteria()
            {
                CustId = custId,
                Mrecommend = mrecommend
            }, custId, 2);

            return View(Constants.EditColossusBetCshtml, setting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditMember(int custId, int recommend)
        {
            var setting = _colossusBetService.GetSetting(new GetMemberSettingCriteria()
            {
                CustId = custId,
                Recommend = recommend
            }, custId, 1);

            return View(Constants.EditColossusBetCshtml, setting);
        }

        [HttpPost]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public JsonResult UpdateSetting(ColossusBetModel model)
        {
            var result = _colossusBetService.UpdateSetting(
                    new UpdatedSettingCriteria()
                    {
                       CustId = model.CustomerInformationPartial.CustId,
                       IsUpdateCommission = model.ComissionPartial.IsCommisionUpdated,
                       IsUpdateCashInShare = model.CashInSharePartial.IsCashInShareUpdated,
                       IsUpdateDisable = model.StatusPartial.IsStatusUpdated,
                       v204Commission_18001 = model.ComissionPartial.CommisionValue,
                       v204CashInShare_18002 = model.CashInSharePartial.CashInShareValue,
                       EnableDownlines = model.StatusPartial.IsDownlinesEnabled,
                       IP = string.Empty,
                       UserID = 8,
                       Language = "E"      
                    }
                );
            return Json(result);
        }

        private void SetTitleForMultipleEdit(ColossusBetModel customerInformationViewModel)
        {
            ////ViewBag.Title = customerInformationViewModel.IsMultipleEdit == 1 ? Resources.LayoutTexts.EditMultipleKeno : Resources.LayoutTexts.UpdateColossusBetSetting;
        }
    }
}
