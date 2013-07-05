namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Newtonsoft.Json;

    public class MasterController : BaseController
    {
        public ActionResult EditMaster(int id, int uplineId)
        {
            var customerSetting = CasinoManager.Instance.MasterCasinoModel(id, uplineId);

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Master/EditMaster.cshtml", customerSetting);
        }

        public ActionResult UpdateMasterPost(int id, int roleId, int uplineId, int uplineRoleId, string ip, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateMaster(id, roleId, uplineId, uplineRoleId, ip, userId, username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}