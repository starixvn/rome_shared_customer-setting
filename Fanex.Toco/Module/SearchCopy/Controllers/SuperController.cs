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
    using System.Configuration;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {        
        /// <summary>
        /// Render search existing super form.
        /// </summary>        
        /// <returns>The ActionResult.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SearchAndCopy")]
        public ActionResult Index()
        {            
            string accIdSessionName = ConfigurationManager.AppSettings["AccIdSessionName"].ToString();
            ViewBag.CurrentUserId = Fanex.Common.Converter.ToInt(System.Web.HttpContext.Current.Session[accIdSessionName]);
            ViewBag.CurrentURL = ConfigurationManager.AppSettings["CurrentURL"].ToString();            

            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml");           
        }

        /// <summary>
        /// Searching a existing super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Return JSONRESULT.</returns>
        public JsonResult SearchExistingSuper(string username)
        {
            var criteria = new SearchExistingUserCriteria
            {
                Uplineid = 0,
                Username = username,
                Custid = -1,
                Issubacc = false
            };

            int userId = SearchExistingUser(criteria);
            
            CustomerSettingView customerSettingView = new CustomerSettingView
            {
                UserId = userId,
                Username = username,
                StatusSports = userId > 0 ? GetSportsStatusByUserName(username) : new HideSports()
            };

            return Json(customerSettingView, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Copy existing super.
        /// </summary>
        /// <param name="criteria">The criteria for copying.</param>
        /// <returns>The JSONRESULT .</returns>
        public JsonResult CopyExistingSuper(CustomerInsMemberCriteria criteria)
        {
            criteria.FirstName = criteria.FirstName ?? string.Empty;
            criteria.LastName = criteria.LastName ?? string.Empty;
            criteria.Phone = criteria.Phone ?? string.Empty;
            criteria.MobilePhone = criteria.MobilePhone ?? string.Empty;
            criteria.Fax = criteria.Fax ?? string.Empty;

            return CopyExistingUser(criteria);            
        }
    }
}
