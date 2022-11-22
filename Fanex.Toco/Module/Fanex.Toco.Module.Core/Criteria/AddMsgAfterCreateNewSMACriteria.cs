using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class AddMsgAfterCreateNewSMACriteria : CriteriaBase
    {
        public AddMsgAfterCreateNewSMACriteria(int custId, int subAccId, int roleid)
        {
            CustId = custId;
            SubAccId = subAccId;
            Roleid = roleid;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public int Roleid { get; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_Ins";

        public override bool IsValid() => CustId > 0;
    }
}