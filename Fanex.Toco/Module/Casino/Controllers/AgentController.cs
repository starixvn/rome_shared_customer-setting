namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Newtonsoft.Json;

    public class AgentController : BaseController
    {
        public ActionResult EditAgent(int id, int uplineId)
        {
            var customerSetting = CasinoManager.Instance.AgentCasinoModel(id, uplineId);

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/EditAgent.cshtml", customerSetting);
        }

        public ActionResult ViewAgentBySuper(int id, int uplineId)
        {
            var customerSetting = CasinoManager.Instance.AgentCasinoModelBySuper(id, uplineId);

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/ViewAgentBySuper.cshtml", customerSetting);
        }

        public ActionResult UpdateAgentPost(int id, int uplineId, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateAgent(id, uplineId, Request.UserHostAddress, userId, username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}