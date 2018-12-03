using System;
using Fanex.Data.Repository;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckCustomerIsSpecialSiteCriteria : CriteriaBase
    {
        public string UserName { get; set; }

        public byte? CheckCustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerIsSpecialSite";
        }

        public override bool IsValid()
            => true;
    }
}
