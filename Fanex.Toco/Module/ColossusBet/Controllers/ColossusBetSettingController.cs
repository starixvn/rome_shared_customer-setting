namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Resources;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Services;
    using Fanex.CustomerSetting.Security.Authenticate;

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

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditSetting(SettingContext settingContext)
        {
            var setting = _colossusBetService.EditSetting(settingContext);
            ViewBag.Title = settingContext.IsMultipleEdit == 1 ? Messages.EditMultiColossusBets : Messages.EditColossusBets;
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "ColossusBetSetting")]
        public JsonResult UpdateSetting(ColossusBetModel model)
        {
            var colossusBetSession = new ColossusBetSession();
            var result = _colossusBetService.UpdateSetting(
                    new UpdatedSettingCriteria()
                    {
                        CustId = model.CustomerInformationPartial.CustId,
                        IsUpdateCommission = model.ComissionPartial.IsCommisionUpdated,
                        IsUpdateCashInShare = model.CashInSharePartial.IsCashInShareUpdated,
                        IsUpdateDisable = model.StatusPartial.IsStatusUpdated,
                        V204Commission_18001 = model.ComissionPartial.CommisionValue,
                        V204CashInShare_18002 = model.CashInSharePartial.CashInShareValue,
                        EnableDownlines = model.StatusPartial.IsDownlinesEnabled,
                        Disable = !model.StatusPartial.IsEnabled,
                        IP = colossusBetSession.Ip,
                        UserID = model.CustomerInformationPartial.UserId,
                        Language = colossusBetSession.Language
                    });

            return Json(result);
        }
    }
}
