using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.ExternalAccount.DTO.Customer.Casino;
using Fanex.ExternalAccount.Service.Impl.Customer.Casino;

namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
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
