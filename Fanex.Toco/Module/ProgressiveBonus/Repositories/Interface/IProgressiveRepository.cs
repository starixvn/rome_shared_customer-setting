// -----------------------------------------------------------------------
// <copyright file="IProgressiveRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  IProgressiveRepository
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface
{
	using System.Collections;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
	using Fanex.NAP.Framework.Data;

	/// <summary>
	/// IProgressiveRepository
	/// </summary>
	public interface IProgressiveRepository
	{
		/// <summary>
		/// Get Progressive Bonus
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
        ProgressiveStatus GetStatusProgressiveBonus(GetProductCriteria criteria);

		/// <summary>
		/// Update Progressive Bonus
		/// </summary>
		/// <param name="criteria">Parameter of stored procedure</param>
		/// <returns>Result code of DB</returns>
		Hashtable UpdateStatusProgressiveBonus(ICriteria criteria);
	}
}