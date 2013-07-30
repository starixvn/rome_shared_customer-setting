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
                FromSystem system = (FromSystem)ProgressiveSection.Instance.System;
                int roleId = AppSession.AdminRoleId;
                if (roleId == 0 && system == FromSystem.Accounting)
                {
                    return true;
                }

				int adminId = AppSession.AdminId;
				string adminUsename = AppSession.AdminUsername;

				string requestUsername = AppSession.RequestUsername;
				int requestRoleId = AppSession.RequestRoleId;
				if (adminId > 0 &&
					!string.IsNullOrEmpty(adminUsename) &&
					!string.IsNullOrEmpty(requestUsername) &&
					roleId - 1 == requestRoleId /* direct downline */)
				{
					
					if (system == FromSystem.Agency)
					{
						IProgressiveService progressiveService = new ProgressiveService();
						bool isInternal = AppSession.IsInternalAgent;
                        bool hasPermission = false;//implement later
						return !hasPermission;
					}

					return true;
				}
			}

			return false;
		}
	}
}