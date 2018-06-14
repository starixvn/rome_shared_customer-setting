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

    public class MasterController : BaseController
    {
        private readonly MasterServices masterServices;
        private const string errorPage = "~/Areas/CustomerSetting/Views/Casino/Error.cshtml";
        private const string editMasterPartialPage = "~/Areas/CustomerSetting/Views/Casino/Master/EditMaster.cshtml";

        public MasterController() : this(null)
        {
        }

        public MasterController(MasterServices masterServices)
        {
            this.masterServices = masterServices ?? new MasterServicesImpl();
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditMaster(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = masterServices.MasterCasinoModel(id, uplineId);
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

            return this.PartialView(editMasterPartialPage, customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateMasterPost(int id, string username, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;

            var ret = masterServices.UpdateMaster(new UpdateCasinoCommand(
                id, 
                username, 
                uplineId, 
                Request.UserHostAddress, 
                userId, 
                actorName,
                AppSession.CurrentSession.FromSystem,
                SystemId,
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), 
                JsonConvert.DeserializeObject<GeneralCasinoCustomer>(customerModel)));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}