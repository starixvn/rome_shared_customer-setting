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
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Master.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Agent()
        {
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Agent.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Member()
        {
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/Member.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult SuperEditMember()
        {
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/SuperEditMember.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult MasterEditMember()
        {
            return View("~/Areas/CustomerSetting/Views/OpusCasino/Setting/MasterEditMember.cshtml");
        }
    }
}
