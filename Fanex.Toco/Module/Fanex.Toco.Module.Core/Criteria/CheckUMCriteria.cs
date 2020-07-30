using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    public class CheckUmCriteria : CriteriaBase
    {
        public CheckUmCriteria(int custId, byte productId, string language)
        {
            CustId = custId;
            ProductId = productId;
            Language = language;
        }

        public int CustId { get; }

        public byte ProductId { get; }

        public byte FunctionType => 1;

        public string Language { get; set; }

        public bool ReturnUMData => false;

        public override string GetSettingKey() => "Acc_CheckUM";

        public override bool IsValid() => CustId > 0;
    }
}