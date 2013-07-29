namespace Fanex.CustomerSetting.Module.ProgressiveModule.Permission
{
	using Fanex.CustomerSetting.Module.Core.Web;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;

	public static class UserPermission
	{
		public static bool CheckProgressiveBonusPermission()
		{
			bool active = ProgressiveSection.Instance.Active;
			if (active)
			{
				int adminId = AppSession.AdminId;
				string adminUsename = AppSession.AdminUsername;
				int roleId = AppSession.AdminRoleId;

				string requestUsername = AppSession.RequestUsername;
				int requestRoleId = AppSession.RequestRoleId;
				if (adminId > 0 &&
					!string.IsNullOrEmpty(adminUsename) &&
					!string.IsNullOrEmpty(requestUsername) &&
					roleId - 1 == requestRoleId /* direct downline */)
				{
					FromSystem system = (FromSystem)ProgressiveSection.Instance.System;
					if (system == FromSystem.Agency)
					{
						IProgressiveService progressiveService = new ProgressiveService();
						bool isInternal = AppSession.IsInternalAgent;
						bool hasPermission = progressiveService.IsDisableProgessiveBonus(adminUsename, adminId, isInternal);
						return !hasPermission;
					}

					return true;
				}
			}

			return false;
		}
	}
}