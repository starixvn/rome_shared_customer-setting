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

    public class SuperController : BaseController
    {
        private readonly SuperServices superServices;
        private const string errorPage = "~/Areas/CustomerSetting/Views/Casino/Error.cshtml";
        private const string editSuperPartialPage = "~/Areas/CustomerSetting/Views/Casino/Super/EditSuper.cshtml";

        public SuperController() : this(null)
        {
        }

        public SuperController(SuperServices superServices)
        {
            this.superServices = superServices ?? new SuperServicesImpl();
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditSuper(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = superServices.SuperCasinoModel(id, uplineId);
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

            return this.PartialView(editSuperPartialPage, customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateSuperPost(int id, string username, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var ret = superServices.UpdateSuper(
                new UpdateCasinoCommand(
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

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : ret.ErrorCode, ret.ErrorMessage });
        }
    }
}