// ************************************************************************************
// <copyright file="RacingSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Thai.Tran date="2013.10.30"">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System.Web.Mvc;
using Fanex.CustomerSetting.Security.Authenticate;

namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Index")]
        public ActionResult Index()
        {
            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml");
        }


        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Index")]
        public ActionResult Angularjs()
        {
            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Angularjs.cshtml");
        }
    }
}
