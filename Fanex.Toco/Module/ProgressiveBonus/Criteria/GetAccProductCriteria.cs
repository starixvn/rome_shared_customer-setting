// -----------------------------------------------------------------------
// <copyright file="GetProductCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Criteria
{
	using Fanex.NAP.Framework.Data;

	public class GetAccProductCriteria : CriteriaBase, ICriteria
	{
		public string Username { get; set; }

		public override string GetSettingKey()
		{
			return "GetStatusOfProgressiveBonus";
		}

		public override bool IsValid()
		{
			return true;
		}
	}

	public class GetAgeProductCriteria : GetAccProductCriteria, ICriteria
	{
		public int UplineId { get; set; }

		public bool IsInternalAgent { get; set; }
	}
}