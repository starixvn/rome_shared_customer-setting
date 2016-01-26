namespace Fanex.CustomerSetting.Module.ColossusBets.Controllers
{
    using System.Web.Mvc;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.ColossusBets.Configurations;
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
            if (ColossusBetsSection.Instance.Configurations["IsViewBetlistDetail"] != null && !Converter.ToBool(ColossusBetsSection.Instance.Configurations["IsViewBetlistDetail"].Value))
            {
                return new EmptyResult();
            }

            var result = new ColossusBetsDetailModel
            {
                MemberChoice = _colossusBetsService.MemberChoice(criteria),
                TicketDetails = _colossusBetsService.BetDetails(criteria)
            };

            return View("~/Areas/ColossusBets/Views/BetDetails/Index.cshtml", result);
        }
    }
}
