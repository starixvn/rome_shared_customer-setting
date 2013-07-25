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

	public interface IProgressiveRepository
	{
        ProgressiveStatus GetStatusOfProgressiveBonus(ICriteria criteria);

		Hashtable UpdateStatusOfProgressiveBonus(ICriteria criteria);
	}
}