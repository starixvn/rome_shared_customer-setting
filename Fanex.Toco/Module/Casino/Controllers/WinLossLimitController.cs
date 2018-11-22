namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Globalization;
    using System.Web.Mvc;
    using Authenticate;
    using Core.Web;
    using Core.Web.Controllers;
    using Entities;
    using Newtonsoft.Json;
    using Security.Authenticate;
    using Services.Impl;
    using Services.Interface;

    public class WinLossLimitController : BaseController
    {
        private readonly MemberWinLimitPartialService service;

        public WinLossLimitController() : this(null)
        {
        }

        internal WinLossLimitController(MemberWinLimitPartialService service)
        {
            this.service = service ?? new MemberWinLimitPartialServiceImpl();
        }

        [HttpPost, AntiXSRFFilter, CasinoUMTimeFilter, AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        public ActionResult Update(int custId, int userId, int subAccId, string winLimit, byte roleId)
        {
            var memberWinLimit = JsonConvert.DeserializeObject<MemberWinLimit>(winLimit);            
            var result = service.UpdateMemberWinLimit(
                custId,
                userId,
                subAccId,
                AppSession.CurrentContext.Ip4Address,
                CultureInfo.CurrentCulture.Name,
                memberWinLimit, roleId);

            return JsonBase(result);
        }
    }
}