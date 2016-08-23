namespace Fanex.Voidbridge.BetDetail.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class GetVoidbridgeBetDetailsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int TransType { get; set; }

        [SpParam(SqlParamName = "RefNo")]
        public long TransId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

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
