namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Newtonsoft.Json;

    public class SuperController : BaseController
    {
        public ActionResult EditSuper(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.SuperCasinoModel(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Super/EditSuper.cshtml", customerSetting);
        }
    }
}