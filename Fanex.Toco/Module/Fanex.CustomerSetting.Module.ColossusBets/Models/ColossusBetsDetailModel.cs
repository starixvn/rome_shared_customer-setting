namespace Fanex.CustomerSetting.Module.ColossusBets.Models
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class ColossusBetsDetailModel
    {
        public ViewMemberChoiceResultModel MemberChoice { get; set; }

        public IEnumerable<ITicket> TicketDetails { get; set; }
    }
}
