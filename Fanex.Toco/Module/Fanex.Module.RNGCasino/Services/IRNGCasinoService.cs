namespace Fanex.Module.RNGCasino.Services
{
    using System.Collections.Generic;
    using Criteria;
    using Models;

    public interface IRNGCasinoService
    {
        IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);
    }
}
