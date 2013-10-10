namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;

    public class CasinoCustomerController : BaseController
    {
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        public ActionResult CustomerInfo(int id, int uplineId)
        {
            var customerInfo = CasinoManager.Instance.GetCustInfoIBC(id, uplineId);
            var customerUpline = new CasinoCustomer();
            
            customerUpline.IBCCustID = uplineId;

            if (uplineId != 0)
            {
                CasinoManager.Instance.GetCustSettingCS(ref customerUpline);
            }
            
            customerInfo.IBCCustID = customerInfo.CasinoSync ? id : uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerInfo);

            customerInfo.UplineRoleId = uplineId != 0 ? customerUpline.RoleId : 5;
            customerInfo.IsAutoReset = !customerInfo.CasinoSync ? true : customerInfo.IsAutoReset;

            if (uplineId != 0 && customerUpline.RoleId == customerInfo.RoleId)
            {
                customerInfo.RoleId = customerUpline.RoleId - 1;
                customerInfo.IBCCustID = id;
            }

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml", customerInfo);
        }
    }
}
