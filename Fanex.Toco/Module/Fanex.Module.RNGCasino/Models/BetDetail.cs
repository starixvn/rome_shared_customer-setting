using System.Collections.Generic;
using System.Linq;

namespace Fanex.Module.RNGCasino.Models
{
    public class BetDetail
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public IEnumerable<TicketDetail> TicketDetails { get; set; }
    }
}
