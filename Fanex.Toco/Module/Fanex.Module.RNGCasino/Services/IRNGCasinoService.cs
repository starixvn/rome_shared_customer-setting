namespace Fanex.Module.RNGCasino.Services
{
    using Criteria;
    using Models;
    using Models.HandHistory;
    using System;
    using System.Collections.Generic;

    public interface IRNGCasinoService
    {
        [Obsolete]
        IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        HandHistoryModel GetHandHistory(GetRNGCasinoResultCriteria criteria);

        HandHistoryModel GetVoidbridgeResult(GetVoidbridgeResultCriteria criteria, string language);
    }
}
