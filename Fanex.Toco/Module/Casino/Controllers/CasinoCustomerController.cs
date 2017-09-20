namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System;
    using System.Web.Mvc;
    using Authenticate;
    using CasinoLibrary.Model.UserException;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Security.Configurations;
    using Fanex.Module.Logs;
    using Fanex.Module.Logs.Services;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CasinoCustomerController : BaseController
    {
        public ActionResult CustomerInfo(int id, int uplineId)
        {
            return PartialView("~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml");
        }

        public ActionResult CustomerData(int id, int uplineId)
        {
            var customerInfo = CasinoManager.Instance.GetCustInfoIBC(id, uplineId);
            var customerUpline = new CasinoCustomer();

            customerUpline.GeneralInfo.IBCCustID = uplineId;

            if (uplineId != 0)
            {
                CasinoManager.Instance.GetCustSettingCS(ref customerUpline);
            }

            customerInfo.GeneralInfo.IBCCustID = customerInfo.GeneralInfo.CasinoSync ? id : uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerInfo);

            customerInfo.GeneralInfo.UplineRoleId = uplineId != 0 ? customerUpline.GeneralInfo.RoleId : 5;
            customerInfo.GeneralInfo.IsAutoReset = !customerInfo.GeneralInfo.CasinoSync ? true : customerInfo.GeneralInfo.IsAutoReset;

            //// For get autoMaster.
            customerInfo.GeneralInfo.IsDisableAutoMPT = customerUpline.GeneralInfo.IsDisableAutoMPT;

            // Not synced case
            if (uplineId != 0 && customerUpline.GeneralInfo.RoleId == customerInfo.GeneralInfo.RoleId)
            {
                customerInfo.GeneralInfo.RoleId = customerUpline.GeneralInfo.RoleId - 1;
                customerInfo.GeneralInfo.UplineRoleId = customerInfo.GeneralInfo.RoleId + 1;
                customerInfo.GeneralInfo.IBCCustID = id;
            }

            // Gets member win limit information.
            ICasinoMemberWinLimitService winLimitService = new CasinoMemberWinLimitService();
            customerInfo.GeneralInfo.MemberWinLimit = winLimitService.GetMemberWinLimit(id);

            var generalCustomer = customerInfo.GeneralInfo;
            var result = JsonConvert.SerializeObject(
                generalCustomer,
                Formatting.None,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            return Content("var customerModel = " + result + ";");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Transfer")]
        [CasinoUMTimeFilter]
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public ActionResult ResetMaxTransfer(int id, string groupName = null)
        {
            var authenticateService = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;

            if (!authenticateService.IsBelong(id, null, groupName))
            {
                return ActionDenied(Request);
            }

            var customerInfo = new CasinoLibrary.SBAgent.Customer();

            try
            {
                var success = customerInfo.ResetMaxTransfer(
                    id,
                    Request.UserHostAddress,
                    AppSession.CurrentSession.UserId,
                    AppSession.CurrentSession.UserName,
                    AppSession.CurrentSession.FromSystem);

                return JsonBase(new { ErrorCode = success ? 0 : 1, ErrorMessage = success ? string.Empty : "Reset max transfer failed" });
            }
            catch (ErrorException casinoEx)
            {
                casinoEx.WriteCasinoException(Formater.LogType.Warning);
                return JsonBase(new { ErrorCode = 9999, ErrorMessage = casinoEx.Message });
            }
            catch (Exception ex)
            {
                return JsonBase(new { ErrorCode = 1, ErrorMessage = ex.Message });
            }
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        //[AntiXSRFFilter] // Comment this attribute when building for NAP & Alpha
        public ActionResult GetCurrentWinLimit(int id, string groupName = null)
        {
            var authenticateService = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;

            if (!authenticateService.IsBelong(id, null, groupName))
            {
                return ActionDenied(Request);
            }

            ICasinoMemberWinLimitService service = new CasinoMemberWinLimitService();

            var result = service.GetCurrentWinLimit(id);

            return JsonBase(new { ErrorCode = 0, ErrorMessage = string.Empty, Result = result });
        }
    }
}