namespace Fanex.CustomerSetting.Module.NG2.Core.Criteria
{
    using Fanex.Data.Repository;

    public class CheckSpecialSitePermissionCriteria : CriteriaBase
    {
        public int UserID { get; set; }

        public int? CustID { get; set; }

        public string CustName { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerIsAllowedAccess";
        }
    }
}
