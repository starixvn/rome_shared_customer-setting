// ************************************************************************************
// <copyright file="RacingSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Racebook.Services;
using Fanex.CustomerSetting.OpusCasino.ViewModels;
using Fanex.CustomerSetting.Security.Authenticate;
using Fanex.CustomerSetting.OpusCasino.Entities;

namespace Fanex.CustomerSetting.OpusCasino.Controllers
{
    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class OpusCasinoSettingController : BaseOpusCasinoSettingController
    {
        private static IOpusSettingService _opusSettingService;

        public OpusCasinoSettingController()
            : this(null)
        {
        }

        public OpusCasinoSettingController(IOpusSettingService opusSettingService)
        {
            _opusSettingService = opusSettingService ?? new OpusSettingService();
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <returns></returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoSuper")]
        public ActionResult OpusCasinoSuper(int custId)
        {
            OpusSetting opus = _opusSettingService.SelectCustomerInfo(custId, 0, 4, 0);
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Super.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoMaster")]
        public ActionResult OpusCasinoMaster(int custId)
        {
            OpusSetting opus = _opusSettingService.SelectCustomerInfo(custId, 4, 3, 0);
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Master.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoAgent")]
        public ActionResult OpusCasinoAgent(int custId)
        {
            OpusSetting opus = _opusSettingService.SelectCustomerInfo(custId, 3, 2, 0);
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Agent.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoMember")]
        public ActionResult OpusCasinoMember(int custId)
        {
            OpusSetting opus = _opusSettingService.SelectCustomerInfo(custId, 2, 1, 0);
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Member.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoSuperEditMember")]
        public ActionResult OpusCasinoSuperEditMember()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/SuperEditMember.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "OpusCasinoMasterEditMember")]
        public ActionResult OpusCasinoMasterEditMember()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/MasterEditMember.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "UpdateSuper")]
        [HttpPost]
        public JsonResult UpdateSuper(OpusSetting opusSetting)
        {
            Result result = _opusSettingService.UpdateSuper(opusSetting);
            return Json(result);
        }
    }
}
