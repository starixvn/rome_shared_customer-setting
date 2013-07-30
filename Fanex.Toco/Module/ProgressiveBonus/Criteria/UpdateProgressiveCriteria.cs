// -----------------------------------------------------------------------
// <copyright file="UpdateProgressiveCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Criteria
{
	using Fanex.NAP.Framework.Data;

	public class UpdateProgressiveCriteria : CriteriaBase, ICriteria
	{
		public int CustId { get; set; }

		public bool Disable { get; set; }

		public bool DisableDownlines { get; set; }

		public int SubAccId { get; set; }

		public int UserId { get; set; }

		public string Language { get; set; }

		public string IP { get; set; }

		public override string GetSettingKey()
		{
            return "CustomerUpdDisableCasinoBonus";
		}

		public override bool IsValid()
		{
			return true;
		}
	}
}