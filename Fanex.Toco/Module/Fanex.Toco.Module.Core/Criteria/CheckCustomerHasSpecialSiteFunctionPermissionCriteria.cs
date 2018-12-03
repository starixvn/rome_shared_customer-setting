using System;
using Fanex.Data.Repository;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckCustomerHasSpecialSiteFunctionPermissionCriteria : CriteriaBase
    {
        public int CustID { get; set; }

        public string FuncName
        {
            get
            {
                return "[AccessSpecialSites]";
            }
        }

        public override string GetSettingKey()
        {
            return "Acc_Con_CheckPermission_All";
        }

        public override bool IsValid()
            => CustID > 0;
    }
}
