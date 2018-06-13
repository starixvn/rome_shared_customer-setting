namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System;
    using System.Web.Mvc;
    using Core.Web.Controllers;
    using Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Security.Authenticate;
    using Security.Configurations;
    using Services.Impl;
    using Services.Interface;

    public class CasinoCustomerController : BaseController
    {
        private readonly CasinoServiceBase casinoManager;

        public CasinoCustomerController()
        {
            casinoManager = new CasinoServiceBase();
        }

        public ActionResult CustomerInfo()
        {
            return PartialView("~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml");
        }

        public ActionResult CustomerData(int id, int uplineId)
        {
            var customerInfo = casinoManager.GetCustInfoIBC(id, uplineId);
            var customerUpline = new CasinoCustomer();

            customerUpline.GeneralInfo.IBCCustID = uplineId;

            if (uplineId != 0)
            {
                casinoManager.GetCustSettingCS(ref customerUpline);
            }

            customerInfo.GeneralInfo.IBCCustID = customerInfo.GeneralInfo.CasinoSync ? id : uplineId;
            casinoManager.GetCustSettingCS(ref customerInfo);

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


        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Casino")]
        [AntiXSRFFilter]
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