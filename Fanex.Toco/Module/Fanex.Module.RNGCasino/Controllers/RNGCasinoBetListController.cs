namespace Fanex.Module.RNGCasino.Controllers
{
    using System;
    using System.Web.Mvc;
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Models;
    using Resources;
    using Services;

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
            var result = GetBetDetail(criteria);
            return View("~/Areas/RNGCasino/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult BetDetailsPaging(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = GetBetDetail(criteria);
            return PartialView("~/Areas/RNGCasino/Views/BetDetails/_Tickets.cshtml", result);
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

        [HttpGet]
        public ActionResult GetFishingWorldResult(GetResultCriteria criteria)
        {
            ViewBag.ProductName = BetListResources.FishingWorld;
            var result = _rngCasinoService.GetResultCriteria(criteria, Session[Session.SessionID + "_LANGUAGE"].ToString());
            return View("~/Areas/RNGCasino/Views/HandHistory_API/Index.cshtml", result);
        }

        [NonAction]
        private BetDetail GetBetDetail(GetRNGCasinoBetDetailsCriteria criteria)
        {
            criteria.PageIndex++;
            if (criteria.PageSize == 0)
            {
                criteria.PageSize = 100;
            }

            var ticketDetails = _rngCasinoService.BetDetails(criteria);

            return new BetDetail
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                TotalRows = criteria.TotalRows,
                TicketDetails = ticketDetails
            };
        }
    }
}
