using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckCustomerHasSpecialSiteFunctionPermissionCriteria : CriteriaBase
    {
        public int CustID { get; set; }

        public string FuncName
        {
            get
            {
                return "[789ySites]";
            }
        }

        public override string GetSettingKey()
        {
            return "Acc_Con_CheckPermission_All";
        }
    }
}
