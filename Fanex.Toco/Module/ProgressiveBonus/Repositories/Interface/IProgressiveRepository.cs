// -----------------------------------------------------------------------
// <copyright file="IProgressiveRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface
{
	using System.Collections;
	using Fanex.NAP.Framework.Data;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;

	public interface IProgressiveRepository
	{
        ProgressiveStatus GetStatusProgressiveBonus(GetProductCriteria criteria);

		Hashtable UpdateStatusProgressiveBonus(ICriteria criteria);
	}
}