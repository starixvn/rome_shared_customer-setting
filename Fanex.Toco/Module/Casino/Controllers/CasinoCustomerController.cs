namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.Module.Casino.Entities;

    public class CasinoCustomerController : BaseController
    {
        public ActionResult CustomerInfo(int id, int uplineId)
        {
            var customerInfo = CasinoManager.Instance.GetCustInfoIBC(id, uplineId);
            var customerUpline = new CasinoCustomer();
            
            customerUpline.IBCCustID = uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerUpline);
            
            customerInfo.IBCCustID = customerInfo.CasinoSync ? id : uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerInfo);

            customerInfo.UplineRoleId = customerUpline.RoleId;
            customerInfo.IsAutoReset = !customerInfo.CasinoSync ? true : customerInfo.IsAutoReset;

            if (customerUpline.RoleId == customerInfo.RoleId)
            {
                customerInfo.RoleId = customerUpline.RoleId - 1;
                customerInfo.IBCCustID = id;
            }

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml", customerInfo);
        }
    }
}
