namespace Fanex.CustomerSetting.Module.ProgressiveModule.Permission
{
	using Fanex.CustomerSetting.Module.Core.Web;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
	using Fanex.NAP.Framework.Data;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl;

	public static class UserPermission
	{
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
							var progressiveStatus = progressiveService.GetStatusProgressiveBonus(new GetProductCriteria {
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

		public static bool CheckProgressiveBonusPermissionWithRole()
		{
			FromSystem system = (FromSystem)ProgressiveSection.Instance.System;

			bool isActive = CheckProgressiveBonusPermission();
			if (isActive && system == FromSystem.Agency)
			{
				int adminRoleid = AppSession.AdminId;
				int userRoleId = AppSession.UserRoleId;

				isActive = adminRoleid - 1 == userRoleId; // direct downline
			}

			return isActive;
		}
	}
}