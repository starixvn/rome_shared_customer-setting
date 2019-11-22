using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        public int RoleId { get; set; }

        [SpParam(Ignored = true)]
        public bool IsDeposit { get; set; }

        public int SettingType => IsDeposit ? 4 : 13;

        public override string GetSettingKey()
        {
            return IsDeposit ? "Acc_Dep_CustSetting_StatusSettingSelect" : "Acc_CustSetting_StatusSettingSelect";
        }
    }
}
