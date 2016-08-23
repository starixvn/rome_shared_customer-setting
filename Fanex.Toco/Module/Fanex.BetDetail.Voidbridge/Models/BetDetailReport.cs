using System.Collections.Generic;

namespace Fanex.Voidbridge.BetDetail.Models
{
    public class BetDetailReport
    {
        public IEnumerable<TicketDetail> TicketDetails { get; set; }

        public int TotalRows { get; set; }
    }
}
