namespace Fanex.Module.RNGCasino.Repositories
{
    using System.Collections.Generic;
    using Criteria;
    using Fanex.BetList.Core.Entities;

    public interface IRNGCasinoRepository
    {
        IEnumerable<Ticket> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);
    }
}
