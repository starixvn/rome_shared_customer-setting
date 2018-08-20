namespace Fanex.CustomerSetting.Module.Casino.Criteria
{
    using NAP.Framework.Data;

    public class UpdateCasinoStatusCriteria : CriteriaBase
    {
        public UpdateCasinoStatusCriteria(
            int custId,
            bool disabled,
            bool synced,
            int userId,
            int subAccountId,
            string ip,
            string language,
            byte systemId)
        {
            CustId = custId;
            Disabled = disabled;
            Synced = synced;
            UserId = userId;
            SubAccountId = subAccountId;
            IP = ip;
            Language = language;
            SystemId = systemId;
        }

        public int CustId { get; }

        [SpParam(SqlParamName = "Disabledcs")]
        public bool Disabled { get; }

        [SpParam(SqlParamName = "Synccs")]
        public bool Synced { get; }

        public int UserId { get; }

        [SpParam(SqlParamName = "Subaccid")]
        public int SubAccountId { get; }

        public string IP { get; }

        public string Language { get; }

        public byte SystemId { get; }

        public override bool IsValid() => CustId > 0 && SubAccountId >= 0 && SystemId > 0;

        public override string GetSettingKey() => "Acc_UpdCSStatus";
    }
}