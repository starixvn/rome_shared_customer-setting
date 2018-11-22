namespace Fanex.CustomerSetting.Module.Casino.Criteria
{
    using Data;
    using Data.Repository;

    public class GetEnabledJackpotProductsCriteria : CriteriaBase
    {
        public GetEnabledJackpotProductsCriteria(int custId, bool includeTestData)
        {
            CustId = custId;
            IncludeTestData = includeTestData;
        }

        public int CustId { get; }

        [SpParam(SqlParamName = "IsTest")]
        public bool IncludeTestData { get; }

        public override string GetSettingKey() => "Acc_GameEnableJackpotSel";

        public override bool IsValid() => CustId > 0;
    }
}
