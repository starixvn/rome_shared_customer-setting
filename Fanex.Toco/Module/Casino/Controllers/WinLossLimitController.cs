namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using Authenticate;
    using Core.Web.Controllers;
    using Entities;
    using Newtonsoft.Json;
    using Security.Authenticate;
    using Services.Impl;
    using Services.Interface;
    using System.Web.Mvc;

    public class WinLossLimitController : BaseController
    {
        private readonly MemberWinLimitService service;

        public WinLossLimitController() : this(null)
        {
        }

        internal WinLossLimitController(MemberWinLimitService service)
        {
            this.service = service ?? new MemberWinLimitServiceImpl();
        }

        [HttpPost, AntiXSRFFilter, CasinoUMTimeFilter, AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Casino")]
        public ActionResult Update(int custId, int userId, int subAccId, string winLimit)
        {
            var memberWinLimit = JsonConvert.DeserializeObject<MemberWinLimit>(winLimit);            
            var result = service.UpdateMemberWinLimit(custId, userId, subAccId, memberWinLimit);

            return JsonBase(result);
        }
    }
}