namespace Fanex.Voidbridge.BetDetail.Services
{
    using System.Collections.Generic;
    using Criteria;
    using Models;
    using Models.HandHistory;

    public interface IVoidbridgeService
    {
        IEnumerable<TicketDetail> BetDetails(GetVoidbridgeBetDetailsCriteria criteria);

        HandHistoryModel GetHandHistory(GetVoidbridgeResultCriteria criteria);
    }
}
