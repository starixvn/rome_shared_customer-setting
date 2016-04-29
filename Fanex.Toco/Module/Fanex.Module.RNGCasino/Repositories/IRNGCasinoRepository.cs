namespace Fanex.Module.RNGCasino.Repositories
{
    using Criteria;
    using Fanex.BetList.Core.Entities;
    using System;
    using System.Collections.Generic;

    [Obsolete]
    public interface IRNGCasinoRepository
    {
        IEnumerable<Ticket> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);
    }
}
