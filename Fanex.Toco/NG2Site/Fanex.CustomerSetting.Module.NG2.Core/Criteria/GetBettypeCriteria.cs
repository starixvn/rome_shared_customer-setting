using Fanex.CustomerSetting.Module.NG2.Core.Models;

namespace Fanex.CustomerSetting.Module.NG2.Core.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetBettypeCriteria : CriteriaBase
    {
        public ProductType Mode { get; set; }

        public override string GetSettingKey()
        {
            return "Core_SelBettype";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
