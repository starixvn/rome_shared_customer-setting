namespace Fanex.CustomerSetting.Module.ColossusBets.Services
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBets.Models;

    public interface IColossusBetsService
    {
        IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria);

        ViewMemberChoiceResultModel MemberChoice(GetColossusBetsBetDetailsCriteria criteria);
    }
}
