using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.ExternalAccount.Service.Impl.Customer.Casino;
using Fanex.ExternalAccount.DTO.Customer.Casino;
using System.Collections.Generic;
using Fanex.CustomerSetting.Module.Casino.ViewModels;

namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    public class MasterController : BaseController
    {
        public ActionResult EditMaster(int id, int uplineId)
        {
            var customerSetting = CasinoManager.Instance.MasterCasinoModel(id, uplineId);

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Master/EditMaster.cshtml", customerSetting);
        }
    }
}