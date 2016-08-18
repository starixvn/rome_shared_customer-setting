namespace Fanex.Voidbridge.BetDetail.Controllers
{
    using System.Web.Mvc;
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Models;
    using Services;

    public class VoidbridgeBetListController : BaseController
    {
        private readonly IVoidbridgeService _voidbridgeCasinoService;

        public VoidbridgeBetListController()
        {
            _voidbridgeCasinoService = new VoidbridgeService();
        }

        [HttpGet]
        public ActionResult BetDetails(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var result = GetBetDetailReport(criteria);

            return View("~/Areas/Voidbridge/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult BetDetailsPaging(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var result = GetBetDetailReport(criteria);

            return PartialView("~/Areas/Voidbridge/Views/BetDetails/_Tickets.cshtml", result);
        }

        [HttpGet]
        public ActionResult GetHandHistory(GetVoidbridgeResultCriteria criteria)
        {
            var result = _voidbridgeCasinoService.GetHandHistory(criteria);

            return View("~/Areas/Voidbridge/Views/HandHistory_API/Index.cshtml", result);
        }

        [NonAction]
        private BetDetailReport GetBetDetailReport(GetVoidbridgeBetDetailsCriteria criteria)
        {
            criteria.PageIndex++;

            if (criteria.PageSize == 0)
            {
                criteria.PageSize = 100;
            }

            return new BetDetailReport
            {
                TicketDetails = _voidbridgeCasinoService.BetDetails(criteria),
                TotalRows = criteria.TotalRows
            };
        }
    }
}
