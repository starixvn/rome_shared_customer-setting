using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckSpecialSitePermissionCriteria : CriteriaBase
    {
        public int UserID { get; set; }

        public int? CustID { get; set; }

        public string CustName { get; set; }

        public byte? CheckCustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerIsAllowedAccess";
        }
    }
}
