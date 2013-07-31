// -----------------------------------------------------------------------
// <copyright file="Product.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  Product
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Permission
{
	using Fanex.CustomerSetting.Module.Core.Web;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl;

	/// <summary>
	/// Check user permission of Progressive Bonus
	/// </summary>
	public static class UserPermission
	{
		/// <summary>
		/// User permission with configuration file and permission of logged in user
		/// </summary>
		/// <returns></returns>
		public static bool CheckProgressiveBonusPermission()
		{
			bool active = ProgressiveSection.Instance.Active;
			if (active)
			{
				FromSystem system = (FromSystem)ProgressiveSection.Instance.System;
				int adminId = AppSession.AdminId;

				if (adminId > 0)
				{
					if (system == FromSystem.Agency)
					{
						bool? isAllow = AppSession.IsAllowProgressiveBonus;
						if (!isAllow.HasValue)
						{
							var progressiveService = new ProgressiveRepository();
							var progressiveStatus = progressiveService.GetStatusProgressiveBonus(
								new GetProductCriteria
							{
								CustId = adminId
							});

							isAllow = !progressiveStatus.DisableCasinoBonus;
							AppSession.IsAllowProgressiveBonus = isAllow;
						}

						return isAllow.Value;
					}

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// User permission with configuration file, permission of logged in user and role
		/// </summary>
		/// <returns></returns>
		public static bool CheckProgressiveBonusPermissionWithRole()
		{
			FromSystem system = (FromSystem)ProgressiveSection.Instance.System;

			bool isActive = CheckProgressiveBonusPermission();
			if (isActive && system == FromSystem.Agency)
			{
				int adminRoleid = AppSession.AdminRoleId;
				int userRoleId = AppSession.UserRoleId;

				isActive = adminRoleid - 1 == userRoleId; // direct downline
			}

			return isActive;
		}
	}
}