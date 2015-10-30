namespace Fanex.CustomerSetting.Module.ColossusBets.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBets.Models;
    using Fanex.CustomerSetting.Module.ColossusBets.Services;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    public class ColossusBetsController : BaseController
    {
        private readonly IColossusBetsService _colossusBetsService;

        public ColossusBetsController()
        {
            _colossusBetsService = new ColossusBetsService();
        }

        [HttpGet]
        public ActionResult BetDetails(GetColossusBetsBetDetailsCriteria criteria)
        {
            var result = new ColossusBetsDetailModel
            {
                MemberChoice = _colossusBetsService.MemberChoice(criteria),
                TicketDetails = _colossusBetsService.BetDetails(criteria)
            };

            return View("~/Areas/ColossusBets/Views/BetDetails/Index.cshtml", result);
        }
    }
}
