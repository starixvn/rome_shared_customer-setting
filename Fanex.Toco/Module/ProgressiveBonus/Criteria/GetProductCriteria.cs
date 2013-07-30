// -----------------------------------------------------------------------
// <copyright file="GetProductCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Criteria
{
	using Fanex.NAP.Framework.Data;

	public class GetProductCriteria : CriteriaBase, ICriteria
	{
		public int CustId { get; set; }

		public override string GetSettingKey()
		{
            return "CustomerSelCasinoBonusStatus";
		}

		public override bool IsValid()
		{
			return true;
		}
	}

	
}