namespace Fanex.CustomerSetting.Module.ColossusBets.Repositories
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.CustomerSetting.Module.ColossusBets.Criteria;

    public interface IColossusBetsRepository
    {
        IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria);
    }
}
