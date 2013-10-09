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
using Fanex.CustomerSetting.Module.Core.Security;
using Fanex.CustomerSetting.OpusCasino.ViewModels;

namespace Fanex.CustomerSetting.OpusCasino.Controllers
{
    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class OpusCasinoSettingController : BaseOpusCasinoSettingController
    {
        
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Super()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Super.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Master()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Master.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Agent()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Agent.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Member()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Member.cshtml", opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult SuperEditMember()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/SuperEditMember.cshtml",opus);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult MasterEditMember()
        {
            OpusSetting opus = new OpusSetting();
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/MasterEditMember.cshtml", opus);
        }
    }
}
