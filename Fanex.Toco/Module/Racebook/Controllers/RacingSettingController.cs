// ************************************************************************************
// <copyright file="RacingSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class RacingSettingController : BaseRacingSettingController
    {

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult Super()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/Super.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult Master()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/Master.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult Agent()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/Agent.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult Member()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/Member.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult SuperEditMember()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/SuperEditMember.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult MasterEditMember()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Setting/MasterEditMember.cshtml");
        }
    }
}
