namespace Fanex.Voidbridge.BetDetail.Repositories
{
    using System.Collections.Generic;
    using Criteria;
    using Fanex.BetList.Core.Entities;

    public interface IVoidbridgeRepository
    {
        IEnumerable<Ticket> BetDetails(GetVoidbridgeBetDetailsCriteria criteria);
    }
}
