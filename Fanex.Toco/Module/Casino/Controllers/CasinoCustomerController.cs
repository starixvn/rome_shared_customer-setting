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
        private readonly CasinoServiceBase casinoServiceBase;
        private const string customerInfoPartialPage = "~/Areas/CustomerSetting/Views/Casino/Customer/CustomerInfo.cshtml";

        public CasinoCustomerController() : this(null)
        {
        }

        public CasinoCustomerController(CasinoServiceBase casinoServiceBase)
        {
            this.casinoServiceBase = casinoServiceBase ?? new CasinoServiceBaseImpl();
        }

        public ActionResult CustomerInfo() => PartialView(customerInfoPartialPage);

        public ActionResult CustomerData(int id, int uplineId)
        {
            var customerInfo = casinoServiceBase.GetCustInfoIBC(id, uplineId);
            var customerUpline = new CasinoCustomer();

            customerUpline.GeneralInfo.IBCCustID = uplineId;

            if (uplineId != 0)
            {
                customerUpline = casinoServiceBase.GetCustSettingCS(customerUpline);
            }

            customerInfo.GeneralInfo.IBCCustID = customerInfo.GeneralInfo.CasinoSync ? id : uplineId;
            customerInfo = casinoServiceBase.GetCustSettingCS(customerInfo);

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
            MemberWinLimitService winLimitService = new MemberWinLimitServiceImpl();
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

            MemberWinLimitService service = new MemberWinLimitServiceImpl();

            var result = service.GetCurrentWinLimit(id);

            return JsonBase(new { ErrorCode = 0, ErrorMessage = string.Empty, Result = result });
        }
    }
}