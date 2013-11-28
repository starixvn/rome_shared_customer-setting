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
        
        public int SearchExistingUser(SearchExistingUserCriteria criteria)
        {
            return _searchCopyService.SearchExistingUser(criteria);
        }

        public HideSports GetSportsStatusByUserName(string username)
        {
            return _searchCopyService.GetSportsStatusByUserName(username);
        }

        public string GetSiteCreditAndCurrencyByUserId(int custid)
        {
            return _searchCopyService.GetSiteCreditAndCurrencyByUserId(new GetSiteByUserIdCriteria { Custid = custid });
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

        public JsonResult CopyExistingUser(CustomerInsMemberCriteria criteria)
        {     
            var result = _searchCopyService.CopyExistingUser(criteria);
            return Json(result, JsonRequestBehavior.AllowGet);
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
