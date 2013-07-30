namespace Fanex.CustomerSetting.Module.ProgressiveModule.Permission
{
	using Fanex.CustomerSetting.Module.Core.Web;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
	using Fanex.NAP.Framework.Data;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;

	public static class UserPermission
	{
		public static bool CheckProgressiveBonusPermission()
		{
			bool active = ProgressiveSection.Instance.Active;
			if (active)
			{
				FromSystem system = (FromSystem)ProgressiveSection.Instance.System;
				int adminId = AppSession.AdminId;
				string adminUsename = AppSession.AdminUsername;

				if (adminId > 0 && !string.IsNullOrEmpty(adminUsename))
				{
					if (system == FromSystem.Agency)
					{
						bool? isAllow = AppSession.IsAllowProgressiveBonus;
						if (!isAllow.HasValue)
						{
							IProgressiveService progressiveService = new ProgressiveService();
							bool isInternal = AppSession.IsInternalAgent;
							bool isDisabled = progressiveService.IsDisableProgessiveBonus(adminUsename, adminId, isInternal);

							isAllow = !isDisabled;
							AppSession.IsAllowProgressiveBonus = isAllow;
						}

						return isAllow.Value;
					}

					return true;
				}
			}

			return false;
		}

		public static bool CheckProgressiveBonusPermissionWithRoles()
		{
			bool active = CheckProgressiveBonusPermission();
			if (active)
			{
				int roleId = AppSession.AdminRoleId;
				int requestRoleId = AppSession.RequestRoleId;
				if (roleId == 0)
				{
					return true;
				}
				else
				{
					if (roleId - 1 == requestRoleId /* direct downline */)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}

			return false;
		}
	}
}