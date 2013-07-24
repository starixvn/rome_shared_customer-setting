using System.Collections.Generic;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface
{
	public interface IProgressiveService
	{
		Progressive GetProducts();

		/// <summary>
		/// 2:update from agent site, 3:update from account site
		/// </summary>
		/// <param name="progressive"></param>
		/// <param name="ip"></param>
		/// <param name="userId"></param>
		/// <param name="userName"></param>
		/// <param name="FromSystem">2:update from agent site, 3:update from account site</param>
		/// <returns></returns>
		List<Result> UpdateProgressive(Progressive progressive);
	}
}