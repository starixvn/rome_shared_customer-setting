namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    using Web;
    using Fanex.NAP.Framework.Data;

    public class Acc_DeniedSiteCurrency_Sel : CriteriaBase
    {

        public int UserId
            => AppSession.CurrentSession.UserId;

        public int mode { get; set; }

        public string DeniedList { get; set; }

        public override string GetSettingKey()
            => "Acc_DeniedSiteCurrency_Sel";

        public override bool IsValid()
            => UserId > 0;
    }
}
