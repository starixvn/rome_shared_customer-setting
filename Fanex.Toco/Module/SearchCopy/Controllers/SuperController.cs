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
using Fanex.CustomerSetting.Security.Authenticate;

namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Racing")]
        public ActionResult Index()
        {
            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml");
        }

        
    }
}
