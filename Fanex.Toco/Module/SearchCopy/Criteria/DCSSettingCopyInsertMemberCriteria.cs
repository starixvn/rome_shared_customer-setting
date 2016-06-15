namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using System.Collections;
    using Core.Web;
    using Fanex.NAP.Framework.Data;

    public class DCSSettingCopyInsertMemberCriteria : CriteriaBase
    {
        public string IpAddress { get; set; }

        public int CustID { get; set; }

        public int ExistingCustID { get; set; }

        public int UserID { get; set; }

        public int SubAccID { get; set; }

        public string ErrMsg { get; set; }

        public int ErrCode { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DCSSettingCopyInsertMember";
        }

        public override Hashtable GetSpParams()
        {
            Hashtable param = new Hashtable();
            param.Add("custid", CustID);
            param.Add("existingcustid", ExistingCustID);
            param.Add("userid", UserID);
            param.Add("subaccid", SubAccID);
            param.Add("ip", IpAddress);
            param.Add("errcode", 0);
            param.Add("errmsg", string.Empty);
            return param;
        }

        public override bool IsValid()
        {
            return this.CustID > 0 && this.ExistingCustID > 0;
        }
    }
}
