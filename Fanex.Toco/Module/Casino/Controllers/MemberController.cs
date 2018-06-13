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

    public class MemberController : BaseController
    {
        private readonly MemberServices memberServices;

        public MemberController() : this(null)
        {
        }

        public MemberController(MemberServices memberServices)
        {
            this.memberServices = memberServices ?? new MemberServicesImpl();
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditMember(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = memberServices.MemberCasinoModel(id, uplineId);
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

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMember.cshtml", customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditMemberByMaster(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = memberServices.MemberCasinoModelByMaster(id, uplineId);
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

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMemberByMaster.cshtml", customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [CasinoUMTimeFilter]
        public ActionResult EditMemberBySuper(int id, int uplineId, string groupName = null, bool isSearchAndCopy = false)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = memberServices.MemberCasinoModelBySuper(id, uplineId);
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

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMemberBySuper.cshtml", customerSetting);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateMemberPost(int id, string username, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;
            var result = memberServices.UpdateMember(
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

            return JsonBase(new { ErrorCode = result.IsSuccess ? 0 : 1, ErrorMessage = result.ErrorMessage });
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateMemberBySuperPost(int id, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;
            var result = memberServices.UpdateMemberBySuper(
                id, 
                uplineId, 
                Request.UserHostAddress, 
                userId, 
                actorName,
                AppSession.CurrentSession.FromSystem, 
                SystemId,
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), 
                JsonConvert.DeserializeObject<GeneralCasinoCustomer>(customerModel));

            return JsonBase(new { ErrorCode = result.IsSuccess ? 0 : 1, ErrorMessage = result.ErrorMessage });
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        [CasinoUMTimeFilter]
        [AntiXSRFFilter]
        public ActionResult UpdateMemberByMasterPost(int id, int uplineId, int userId, string actorName, string settingModel, string customerModel, string groupName = null)
        {
            if (!IsBelong(id, groupName))
            {
                return ActionDenied(Request);
            }

            actorName = AppSession.SubAccountId.Equals(0) ? actorName : AppSession.SubAccountName;
            var result = memberServices.UpdateMemberByMaster(
                id, 
                uplineId, 
                Request.UserHostAddress, 
                userId, 
                actorName,
                AppSession.CurrentSession.FromSystem, 
                SystemId,
                JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), 
                JsonConvert.DeserializeObject<GeneralCasinoCustomer>(customerModel));

            return JsonBase(new { ErrorCode = result.IsSuccess ? 0 : 1, ErrorMessage = result.ErrorMessage });
        }

        [NonAction]
        private bool IsBelong(int id, string groupName)
        {
            var authenticateService = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;

            return authenticateService.IsBelong(id, null, groupName);
        }
    }
}