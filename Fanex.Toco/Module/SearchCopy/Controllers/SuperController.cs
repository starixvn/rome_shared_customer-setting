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
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Module.SearchCopy.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {        
        /// <summary>
        /// Render supper settings.
        /// </summary>
        /// <param name="username">The super's name.</param>
        /// <returns>The ActionResult.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Index")]
        public ActionResult Index(string username)
        {
            CustomerSettingView customerSettingView = new CustomerSettingView
            {
                Username = string.IsNullOrEmpty(username) ? string.Empty : username,
                StatusSports = string.IsNullOrEmpty(username) ? new DisableSports() : GetSportsStatusByUserName(username)
            };

            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml", customerSettingView);           
        }

        /// <summary>
        /// Search a existing super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Return True if success else return False.</returns>
        public int SearchExistingSuper(string username)
        {
            var criteria = new SearchExistingUserCriteria
            {
                uplineid = 0,
                username = username,
                custid = -1,
                issubacc = false
            };

            return SearchExistingUser(criteria);
        }

        public JsonResult CopyExistingSuper(CustomerInsMemberCriteria criteria)
        {
            criteria.FirstName = criteria.FirstName ?? string.Empty;
            criteria.LastName = criteria.LastName ?? string.Empty;
            criteria.Phone = criteria.Phone ?? string.Empty;
            criteria.MobilePhone = criteria.MobilePhone ?? string.Empty;
            criteria.Fax = criteria.Fax ?? string.Empty;

            var result = _searchCopyService.CopyExistingUser(criteria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
