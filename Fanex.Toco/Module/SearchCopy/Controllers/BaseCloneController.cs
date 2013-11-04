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
    using System.Data;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.SearchCopy.App_Start;
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

        public int SearchExistingUser(string username)
        {
            return _searchCopyService.SearchExistingUser(username);
        }

        public DataTable GetCustomerInfo(string username)
        {            
            return _searchCopyService.GetCustomerInfo(username);
        }      
     
        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page.
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            ////AppSession.AccId = 8;
            ResourceHelper.SetCulture();
            var rootUrl = Url.Content("~") + filterContext.RouteData.DataTokens["area"].ToString() + "/" + ModuleRegistrator.Module + "/";
            ViewBag.RootUrl = rootUrl;
            base.OnActionExecuting(filterContext);
        }
    }
}
