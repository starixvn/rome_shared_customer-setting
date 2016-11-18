namespace Fanex.Module.RNGCasino.Controllers
{
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Resources;
    using Services;
    using System;
    using System.Web.Mvc;

    public class RNGCasinoBetListController : BaseController
    {
        private readonly IRNGCasinoService _rngCasinoService;

        public RNGCasinoBetListController()
        {
            _rngCasinoService = new RNGCasinoService();
        }

        [HttpGet]
        [Obsolete]
        public ActionResult BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = _rngCasinoService.BetDetails(criteria);
            return View("~/Areas/RNGCasino/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult BetDetailsPaging(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var details = _rngCasinoService.BetDetails(criteria);
            return PartialView("~/Areas/RNGCasino/Views/BetDetails/_Tickets.cshtml", details);
        }

        [HttpGet]
        public ActionResult GetHandHistory(GetRNGCasinoResultCriteria criteria)
        {
            ViewBag.ProductName = BetListResources.Product_6;
            var result = _rngCasinoService.GetHandHistory(criteria);
            return View("~/Areas/RNGCasino/Views/HandHistory_API/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult GetVoidbridgeResult(GetVoidbridgeResultCriteria criteria)
        {
            ViewBag.ProductName = BetListResources.Voidbridge;
            var result = _rngCasinoService.GetVoidbridgeResult(criteria, Session[Session.SessionID + "_LANGUAGE"].ToString());
            return View("~/Areas/RNGCasino/Views/HandHistory_API/Index.cshtml", result);
        }
    }
}
