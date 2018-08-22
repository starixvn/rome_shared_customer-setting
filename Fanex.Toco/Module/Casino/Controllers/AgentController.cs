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
    using Services.Interface;
    using ViewModels;

    public class AgentController : BaseController
    {
        private readonly AgentServices agentServices;
        private const string errorPage = "~/Areas/CustomerSetting/Views/Casino/Error.cshtml";
        private const string editAgentPartialPage = "~/Areas/CustomerSetting/Views/Casino/Agent/EditAgent.cshtml";
        private const string viewAgentBySuperPartialPage = "~/Areas/CustomerSetting/Views/Casino/Agent/ViewAgentBySuper.cshtml";

        public AgentController() : this(null)
        {
        }

        public AgentController(AgentServices agentServices)
        {
            this.agentServices = agentServices ?? new AgentServicesImpl();
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
                customerSetting = agentServices.AgentCasinoModel(id, uplineId);
                customerSetting.IsReadOnly = isSearchAndCopy;
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);

                return this.PartialView(errorPage, casinoEx.Message);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Warning);

                return this.PartialView(errorPage, ex.Message);
            }

            return this.PartialView(editAgentPartialPage, customerSetting);
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
                customerSetting = agentServices.AgentCasinoModelBySuper(id, uplineId);
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);

                return this.PartialView(errorPage, casinoEx.Message);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Warning);

                return this.PartialView(errorPage, ex.Message);
            }

            return this.PartialView(viewAgentBySuperPartialPage, customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateAgentPost(
            int id, 
            string username, 
            int uplineId, 
            int userId, 
            string actorName, 
            string settingModel, 
            string customerModel,
            string groupName = null)
        {
            var obj = System.Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;

            var ret = agentServices.UpdateAgent(new UpdateCasinoCommand(
                id,
                username,
                uplineId,
                AppSession.CurrentContext.Ip4Address,
                userId,
                actorName,
                AppSession.CurrentSession.FromSystem,
                SystemId,
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel),
                JsonConvert.DeserializeObject<GeneralCasinoCustomer>(customerModel)));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ret.ErrorMessage });
        }
    }
}