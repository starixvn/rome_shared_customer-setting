namespace Fanex.CustomerSetting.Module.Casino.Criteria
{
    using Data;
    using Data.Repository;

    public class AllowedDownlinesCriteria : CriteriaBase
    {
        public AllowedDownlinesCriteria(int custId, byte custRole)
        {
            CustId = custId;
            CustRole = custRole;
        }

        public int CustId { get; }

        [SpParam(SqlParamName = "RoleId")]
        public byte CustRole { get; }

        public bool IsExecRep { get; } = true;

        public override bool IsValid()
            => CustId > 0 && CustRole > 0 && CustRole < 5;

        public override string GetSettingKey() => "Acc_CustomerSelCheckAllowedDownlines";
    }
}