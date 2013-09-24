// ************************************************************************************
// <copyright file="SuperController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) SuperController.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Security;
    using Fanex.CustomerSetting.Module.Racebook.Entities;

    /// <summary>
    /// Class SuperController.
    /// </summary>
    public class SuperController : BaseRacingController
    {
        public SuperController()
        {
            CurrentRole = PageRole.Roles.Super;
        }

        /// <summary>
        /// Default page.
        /// </summary>
        /// <returns>Return the default content.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Index()
        {
            return View("~/Areas/CustomerSetting/Views/Racebook/Super/Index.cshtml");
        }
    }
}
