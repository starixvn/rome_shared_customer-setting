namespace Fanex.Voidbridge.BetDetail.Controllers
{
    using System.Web.Mvc;
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Services;

    public class VoidbridgeBetListController : BaseController
    {
        private readonly IVoidbridgeService _rngCasinoService;

        public VoidbridgeBetListController()
        {
            _rngCasinoService = new VoidbridgeService();
        }

        [HttpGet]
        public ActionResult BetDetails(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var result = _rngCasinoService.BetDetails(criteria);
            return View("~/Areas/Voidbridge/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult GetHandHistory(GetVoidbridgeResultCriteria criteria)
        {
            var result = _rngCasinoService.GetHandHistory(criteria);
            return View("~/Areas/Voidbridge/Views/HandHistory_API/Index.cshtml", result);
        }
    }
}
