namespace Fanex.Module.RNGCasino.Criteria
{
    using Fanex.NAP.Framework.Data;
    using System;

    [Obsolete]
    public class GetRNGCasinoBetDetailsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int TransType { get; set; }

        [SpParam(SqlParamName = "RefNo")]
        public long TransId { get; set; }

        public override string GetSettingKey()
        {
            return "BetList_MicroGaming_BetDetails";
        }

        public override bool IsValid()
        {
            return TransId > 0;
        }
    }
}
