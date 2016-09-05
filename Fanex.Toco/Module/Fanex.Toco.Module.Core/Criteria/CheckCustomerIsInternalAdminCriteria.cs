using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckCustomerIsInternalAdminCriteria : CriteriaBase
    {
        public int CustID { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerIsInternalAdmin";
        }
    }
}
