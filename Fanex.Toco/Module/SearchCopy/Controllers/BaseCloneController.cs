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
    using System;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.SearchCopy.App_Start;
    using Fanex.CustomerSetting.Module.SearchCopy.Services;
    using Fanex.Logging;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTEngine.Services;
    using Fanex.Tarus.Customer.CreditCustomer.Business;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Class BaseCloneController.
    /// </summary>
    public class BaseCloneController : BaseController
    {
        protected static ICustomerSettingService _customerSettingService;
        protected static ICustomerService _customerService;
        private static ISearchCopyService _searchCopyService;        

        public BaseCloneController()
            : this(null)
        {
        }

        public BaseCloneController(ISearchCopyService searchCopyService)
        {
            _customerSettingService = new CustomerSettingService();
            _customerService = new CustomerService();
            _searchCopyService = searchCopyService ?? new SearchCopyService();            
        }

        /// <summary>
        /// Get sports book settings.
        /// </summary>
        /// <param name="custId">The customer Id.</param>
        /// <returns>The JSON object.</returns>
        [HttpPost]
        public JsonResult GetSportsbookSettings(int custId)
        {
            var sportsbookSettings = _searchCopyService.GetSportsbookSettings(custId);

            return Json(sportsbookSettings);
        }

        /// <summary>
        /// This function will return data of customer object depend on constrain of up-line
        /// </summary>
        /// <returns>String was serialized of customer object</returns>
        [HttpPost]
        public JsonResult GetCustomerObject(RequestContext requestContext)
        {
            string customerObject = string.Empty;
            try
            {
                //format json return
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //get default value for add new member
                ICustomer customer = GetCustomerForLaterAction(requestContext);
                customerObject = JsonConvert.SerializeObject(customer, Formatting.Indented, settings);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
            }
            return Json(customerObject);
        }

        /// <summary>
        /// This Action will be Override by Child Class Derive from this
        /// </summary>
        /// <param name="requestContext">Context associate with Form Edit or Add New</param>
        /// <returns>Return Customer with information</returns>
        [NonAction]
        public virtual ICustomer GetCustomerForLaterAction(IRequestContext requestContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Default serializer for send data to client
        /// </summary>
        /// <returns>Format setting of Json</returns>
        [NonAction]
        protected JsonSerializerSettings GetJsonSerializerSetting()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
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
