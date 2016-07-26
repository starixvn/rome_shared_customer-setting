namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using Authenticate;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Security.Configurations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Web.Mvc;
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

            customerUpline.IBCCustID = uplineId;
            
            if (uplineId != 0)
            {
                CasinoManager.Instance.GetCustSettingCS(ref customerUpline);
            }

            customerInfo.IBCCustID = customerInfo.CasinoSync ? id : uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref customerInfo);

            customerInfo.UplineRoleId = uplineId != 0 ? customerUpline.RoleId : 5;
            customerInfo.IsAutoReset = !customerInfo.CasinoSync ? true : customerInfo.IsAutoReset;

            //// For get autoMaster.
            customerInfo.IsDisableAutoMPT = customerUpline.IsDisableAutoMPT;
            
            // Not synced case
            if (uplineId != 0 && customerUpline.RoleId == customerInfo.RoleId)
            {
                customerInfo.RoleId = customerUpline.RoleId - 1;
                customerInfo.UplineRoleId = customerInfo.RoleId + 1;
                customerInfo.IBCCustID = id;
            }

            // Gets member win limit information.
            ICasinoMemberWinLimitService winLimitService = new CasinoMemberWinLimitService();
            customerInfo.MemberWinLimit = winLimitService.GetMemberWinLimit(id);

            var result = JsonConvert.SerializeObject(
                customerInfo,
                Formatting.None,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

            return this.Content("var customerModel = " + result + ";");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Transfer")]
        [CasinoUMTimeFilter]
        public ActionResult ResetMaxTransfer(int id, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            var customerInfo = new CasinoLibrary.SBAgent.Customer();
            var success = customerInfo.ResetMaxTransfer(
                id, 
                Request.UserHostAddress, 
                AppSession.CurrentSession.UserId, 
                AppSession.CurrentSession.UserName,
                AppSession.CurrentSession.FromSystem);

            return this.JsonBase(new { ErrorCode = success ? 0 : 1, ErrorMessage = success ? string.Empty : "Reset max transfer failed" });
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        public ActionResult GetCurrentWinLimit(int id, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            ICasinoMemberWinLimitService service = new CasinoMemberWinLimitService();

            var result = service.GetCurrentWinLimit(id);

            return this.JsonBase(new { ErrorCode = 0, ErrorMessage = string.Empty, Result = result });
        }
    }
}