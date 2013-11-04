// --------------------------------------------------------------------------
// <copyright file="SuperController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>
namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    using System;
    using System.Data;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.SearchCopy.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {
        /// <summary>
        /// Search a existing super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Return True if success else return False.</returns>
        public int SearchExistingSuper(string username)
        {
            return SearchExistingUser(username);
        }

        /// <summary>
        /// Render supper settings.
        /// </summary>
        /// <param name="username">The super's name.</param>
        /// <returns>The ActionResult.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Index")]
        public ActionResult Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                CustomerSettingView customerSettingView = new CustomerSettingView
                {
                    username = string.Empty,
                    EnableSports = new EnableSports()
                };

                return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml", customerSettingView);
            }
            else
            {
                DataTable customerStatus = GetCustomerInfo(username);

                EnableSports enableSports = new EnableSports
                {
                    DisableSportsbook = false, ////alway show
                    DisableSportsbookMinPT = false, ////alway show
                    DisableRacing = Convert.ToBoolean(customerStatus.Rows[0]["disablehorseracing"]),
                    DisableRacingMinPT = Convert.ToBoolean(customerStatus.Rows[0]["disablehorseracing"]),
                    DisableCasino = Convert.ToBoolean(customerStatus.Rows[0]["disablecasino"]),
                    DisableVirtualSports = Convert.ToBoolean(customerStatus.Rows[0]["disablevirtualsports"]),
                    DisableBingo = Convert.ToBoolean(customerStatus.Rows[0]["disablebingo"]),
                    DisableLiveCasino = Convert.ToBoolean(customerStatus.Rows[0]["disablelivecasino"])
                };

                CustomerSettingView customerSettingView = new CustomerSettingView
                {
                    username = username,
                    EnableSports = enableSports
                };

                return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml", customerSettingView);
            }
        }    
    }
}
