using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectDCSProductStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int ProductId { get; set; }

        public int RoleId { get; set; }

        public int SettingType
        {
            get
            {
                return 13;
            }
        }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_StatusSettingSelect";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
