// -----------------------------------------------------------------------
// <copyright file="IProgressiveService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  IProgressiveService
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface
{
	using System.Collections.Generic;
	using Fanex.CustomerSetting.Module.Core.Models;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;

	/// <summary>
	/// Progressive Service
	/// </summary>
	public interface IProgressiveService
	{
		/// <summary>
		/// Get Progressive Bonus
		/// </summary>
		/// <param name="custId"></param>
		/// <returns></returns>
		Progressive GetProducts(int custId);

		/// <summary>
		/// Update Progressive Bonus
		/// </summary>
		/// <param name="progressive"></param>
		/// <returns></returns>
		List<Result> UpdateProgressive(Progressive progressive);
	}
}