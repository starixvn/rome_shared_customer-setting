using System;

namespace Fanex.Module.Statement.Models.StatementReport
{
    public class RewardStatementModel
    {
        public DateTime StatementDate { get; set; }

        public string StatementDateString { get; set; }

        public DataColumnModel OpeningBalance { get; set; }

        public DataColumnModel TransferAmount { get; set; }

        public DataColumnModel AccumulatedTotal { get; set; }
    }
}
