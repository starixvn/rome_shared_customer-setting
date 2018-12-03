using System;
using Fanex.Data.Repository;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckCustomerIsInternalAdminCriteria : CriteriaBase
    {
        public int CustID { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerIsInternalAdmin";
        }

        public override bool IsValid()
            => CustID > 0;
    }
}
