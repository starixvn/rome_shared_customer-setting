namespace Fanex.Module.RNGCasino.Services
{
    using System.Collections.Generic;
    using Criteria;
    using Models;
    using Models.HandHistory;

    public interface IRNGCasinoService
    {
        IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        HandHistoryModel GetHandHistory(GetRNGCasinoResultCriteria criteria);
    }
}
