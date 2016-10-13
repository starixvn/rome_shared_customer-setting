using System.Collections.Generic;
using Fanex.BetList.Core.Entities;
using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
using Fanex.CustomerSetting.Module.ColossusBets.Models;

namespace Fanex.CustomerSetting.Module.ColossusBets.Services
{
    public interface IColossusBetsService
    {
        IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria);

        ViewMemberChoiceResultModel MemberChoice(GetColossusBetsBetDetailsCriteria criteria);
    }
}
