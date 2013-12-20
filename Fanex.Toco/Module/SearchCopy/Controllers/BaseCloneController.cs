// --------------------------------------------------------------------------
// <copyright file="BaseCloneController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.SearchCopy.App_Start;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Services;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Class BaseCloneController.
    /// </summary>
    public class BaseCloneController : BaseController
    {
        private static ISearchCopyService _searchCopyService;

        public BaseCloneController()
            : this(null)
        {
        }

        public BaseCloneController(ISearchCopyService searchCopyService)
        {
            _searchCopyService = searchCopyService ?? new SearchCopyService();
        }

        protected ISearchCopyService SearchCopyServiceInstance
        {
            get { return _searchCopyService ?? new SearchCopyService(); }
        }

        /// <summary>
        /// Get client IP.
        /// </summary>
        /// <returns>The client IP.</returns>
        public string GetClientIP()
        {
            string ip = string.Empty;

            if (System.Web.HttpContext.Current.Request.IsLocal)
            {
                ip = "127.0.0.1";
            }
            else
            {
                ip = Fanex.Common.Converter.ToString2(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }

            return ip;
        }

        public int SearchExistingUser(SearchExistingUserCriteria criteria)
        {
            return _searchCopyService.SearchExistingUser(criteria);
        }

        public HideSports GetSportsStatusByUserName(string username)
        {
            return _searchCopyService.GetSportsStatusByUserName(username);
        }

        public JsonResult GetCustomerInfoByUserId(int custid)
        {
            var custInfo = _searchCopyService.GetCustomerInfoByUserId(new GetCustomerInfoByUserIdCriteria { Custid = custid });
            return Json(custInfo, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get sites.
        /// </summary>        
        /// <returns>Return list of sites.</returns>
        public JsonResult GetSites()
        {
            var sites = _searchCopyService.GetSitesExistingSuper(new GetSitesCriteria());
            return Json(sites, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page.
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var rootUrl = Url.Content("~") + filterContext.RouteData.DataTokens["area"].ToString() + "/" + ModuleRegistrator.Module + "/";
            ViewBag.RootUrl = rootUrl;
            base.OnActionExecuting(filterContext);
        }
    }
}
