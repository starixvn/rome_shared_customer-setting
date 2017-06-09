using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        public int RoleId { get; set; }

        public const int SettingType = 13;

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_StatusSettingSelect";
        }
    }
}
