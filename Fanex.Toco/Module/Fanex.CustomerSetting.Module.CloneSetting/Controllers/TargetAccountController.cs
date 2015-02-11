// ************************************************************************************
// <copyright file="TargetAccountController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) TargetAccountController.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.CloneSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.CloneSetting.Entities;
    using Fanex.CustomerSetting.Module.CloneSetting.Services.Impl;
    using Fanex.CustomerSetting.Module.CloneSetting.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TargetAccountController : BaseController
    {
        [HttpGet, AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "CloneSetting")]
        public ActionResult Index()
        {
            return View("~/Areas/CustomerSetting/Views/CloneSetting/Index.cshtml", new CloneAccount());
        }

        [HttpPost, ActionName("Index"), AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "CloneSetting")]
        public ActionResult Clone(CloneAccount model)
        {
            ICloneAccountService service = new CloneAccountService();
            var result = service.Clone(model);

            return Json(result);
        }
    }
}