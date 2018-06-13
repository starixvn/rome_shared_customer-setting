namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;
    using Authenticate;
    using CasinoLibrary.Model.UserException;
    using Core.Web;
    using Core.Web.Controllers;
    using Entities;
    using Logging.Extensions.Web;
    using Newtonsoft.Json;
    using Security.Authenticate;
    using Security.Configurations;
    using Services.Impl;
    using ViewModels;

    public class AgentController : BaseController
    {
        private readonly CasinoManager casinoManager;

        public AgentController()
        {
            casinoManager = new CasinoManager();
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditAgent(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = casinoManager.AgentCasinoModel(id, uplineId);
                customerSetting.IsReadOnly = isSearchAndCopy;
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);

                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml", casinoEx.Message);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Warning);

                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml", ex.Message);
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/EditAgent.cshtml", customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult ViewAgentBySuper(int id, int uplineId, string groupName = null)
        {
            var obj = System.Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = casinoManager.AgentCasinoModelBySuper(id, uplineId);
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);

                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml", casinoEx.Message);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Warning);

                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml", ex.Message);
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Agent/ViewAgentBySuper.cshtml", customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateAgentPost(int id, string username, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            var obj = System.Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;

            var ret = casinoManager.UpdateAgent(
                id,
                username,
                uplineId,
                Request.UserHostAddress,
                userId,
                actorName,
                AppSession.CurrentSession.FromSystem,
                SystemId,
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel),
                JsonConvert.DeserializeObject<GeneralCasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}