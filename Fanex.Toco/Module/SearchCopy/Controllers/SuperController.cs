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
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.Tarus.Customer.CreditCustomer.Entities;
    using Fanex.PTEngine.BusinessObjects;
    using Newtonsoft.Json;
    using Sunplus.Accounting.Core.Common;    

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Index")]
        public ActionResult Index(int custId = 1373389, string uname = "test")
        {
            CustomerSettingView customerSettingView = new CustomerSettingView
            {
                TitlePage = "Super: " + uname,
                IsInsertPage = false,
                IsSuperSetting = true,
                IsMasterSetting = false,
                IsAgentSetting = false,
                IsMemberSetting = false,
                CustId = custId,
                CustUserName = uname,
                CanEditCredit = _customerSettingService.CheckPermissionEditCredit()
            };

            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml", customerSettingView);
        }

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="requestContext">Constext Of Get customer</param>
        /// <returns>Customer Object</returns>
        public override ICustomer GetCustomerForLaterAction(IRequestContext requestContext)
        {
            return _customerService.GetCustomerForCompanyEditSuper(requestContext);
        }

        /// <summary>
        /// This function will return a current context object
        /// </summary>
        /// <returns>Current context object</returns>
        [HttpPost]
        public JsonResult GetContextObject(int superId)
        {
            //init context for edit supper
            IRequestContext currentContext = new RequestContext()
            {
                CustId = superId,
                TargetCustId = superId,
                RoleId = 0,
                TargetRoleId = 4,
                //FormId = FormType.EditPT,
                SubAccId = 0,
                AdminId = AppSession.AccId
            };

            return Json(JsonConvert.SerializeObject(currentContext, Formatting.Indented, GetJsonSerializerSetting()));
        }
    }
}
