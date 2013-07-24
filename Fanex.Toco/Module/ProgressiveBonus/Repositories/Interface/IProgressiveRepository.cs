// -----------------------------------------------------------------------
// <copyright file="IProgressiveRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface
{
	using System.Collections;
	using Fanex.NAP.Framework.Data;

	public interface IProgressiveRepository
	{
		dynamic GetStatusOfProgressiveBonus(ICriteria criteria);

		Hashtable UpdateStatusOfProgressiveBonus(ICriteria criteria);
	}
}