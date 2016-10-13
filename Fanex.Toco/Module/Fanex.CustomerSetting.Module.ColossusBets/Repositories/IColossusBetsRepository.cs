using System.Collections.Generic;
using Fanex.BetList.Core.Entities;
using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.ColossusBets.Repositories
{
    public interface IColossusBetsRepository
    {
        IEnumerable<ITicket> BetDetails(ICriteria criteria);

        string GetSiteIdByCustomerId(ICriteria criteria);
    }
}
