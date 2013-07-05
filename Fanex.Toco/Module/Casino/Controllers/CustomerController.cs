namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    public class CustomerController : BaseController
    {
        public ActionResult CustomerInfo(int id, int uplineId)
        {
            var customerInfo = CasinoManager.Instance.GetCustInfoIBC(id, uplineId);
            customerInfo.IBCCustID = customerInfo.CasinoSync ? id : uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerInfo);

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml", customerInfo);
        }
    }
}
