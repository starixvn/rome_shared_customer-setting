namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Controllers
{
    using System;
    using System.Web.Mvc;
    using Criteria;
    using Entities;
    using Resources;
    using Security.Authenticate;
    using Services;

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
            setting.CustomerInformationPartial.ReadOnly = settingContext.ReadOnly;
            ViewBag.Title = settingContext.IsMultipleEdit == 1 ? Messages.EditMultiColossusBets : Messages.EditColossusBets;
            return View(Constants.EditColossusBetCshtml, setting);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "ColossusBetSetting")]
       // [AntiXSRFFilter] // Comment this attribute when buiding for NAP & Alpha
        public JsonResult UpdateSetting(ColossusBetModel model)
        {
            var colossusBetSession = new ColossusBetSession();
            model.CashInSharePartial.CashInShareValue = model.CashInSharePartial.CashInShareValue / 100;
            model.ComissionPartial.CommisionValue = model.ComissionPartial.CommisionValue / 100;

            var result = _colossusBetService.UpdateSetting(
                    new UpdatedSettingCriteria
                    {
                        CustId = model.CustomerInformationPartial.CustId,
                        IsUpdateCommission = model.ComissionPartial.IsCommisionUpdated,
                        IsUpdateCashInShare = model.CashInSharePartial.IsCashInShareUpdated,
                        IsUpdateDisable = model.StatusPartial.IsStatusUpdated,
                        V204Commission_18001 = model.ComissionPartial.CommisionValue,
                        V204CashInShare_18002 = model.CashInSharePartial.CashInShareValue,
                        Disable = !model.StatusPartial.IsEnabled,
                        EnableDownlines = GetEnableDownlineValue(model.StatusPartial),
                        IP = colossusBetSession.Ip,
                        UserID = model.CustomerInformationPartial.UserId,
                        Language = colossusBetSession.Language
                    });

            return Json(result);
        }

        private static object GetEnableDownlineValue(Status status)
        {
            if (status.IsHasDownlines)
            {
                return status.IsDownlinesEnabled;
            }

            return DBNull.Value;
        }
    }
}