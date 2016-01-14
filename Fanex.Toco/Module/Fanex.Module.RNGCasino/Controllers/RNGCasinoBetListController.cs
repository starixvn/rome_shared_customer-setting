namespace Fanex.Module.RNGCasino.Controllers
{
    using System.Web.Mvc;
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Services;

    public class RNGCasinoBetListController : BaseController
    {
        private readonly IRNGCasinoService _rngCasinoService;

        public RNGCasinoBetListController()
        {
            _rngCasinoService = new RNGCasinoService();
        }

        [HttpGet]
        public ActionResult BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = _rngCasinoService.BetDetails(criteria);
            return View("~/Areas/RNGCasino/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult GetHandHistory(GetRNGCasinoResultCriteria criteria)
        {
            var result = _rngCasinoService.GetHandHistory(criteria);
            return View("~/Areas/RNGCasino/Views/HandHistory/Index.cshtml", result);
        }
    }
}
