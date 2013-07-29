// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.11.12">Created</change>
// </history>

using System.Diagnostics.CodeAnalysis;

namespace Fanex.CustomerSetting.Module.Core
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public static class Constants
    {
        public const string SHORT_DATE_FORMAT = "MMM dd, yyyy";
        public const string MONTH_YEAR_DATE_FORMAT = "MMM yyyy";
        public const string SHORT_DATE_FORMAT_WITH_SLASH = "MM/dd/yyyy";
        public const string LONG_DATE_FORMAT_WITH_SLASH = "MM/dd/yyyy hh:mm:ss tt";
        public const string MONTH_YEAR_DATE_FORMAT_WITH_SLASH = "MM/yyyy";
        public const string LONG_DATE_FORMAT = "dd MMM, yyyy hh:mm:ss tt";
        public const string CURRENT_LOG_IN_USER = "Contants.AppSession.CurrentLogInUser";
        public const string CURRENT_USER_MENU = "Contants.AppSession.CurrentUserMenu";
        public const string CURRENT_USER_ROLE_ACCESS = "Contants.AppSession.CurrentUserRoleAccess";
        public const string CURRENT_WORKING_ON_MENU = "Contants.AppSession.CurrentWorkingOnMenu";
        public const string CURRENT_ACTION_NAME = "Contants.AppSession.CurrentActionName";
        public const string TWO_DECIMAL_DIGIT_FORMAT = ".00";
        public const string FOUR_DECIMAL_DIGIT_FORMAT = ".0000";
        public const string SP_PARAMS_SEPERATOR = ",";
        public const string VIEW_IP_VN = "ViewIPVN";

		public const string ADMIN_ID = "Contants.AppSession.AdminId";
		public const string ADMIN_USERNAME = "Contants.AppSession.AdminUsername";
		public const string ADMIN_ROLEID = "Contants.AppSession.AdminRoleId";
		public const string IS_ALLOW_PROGRESSIVE_BONUS = "Contants.AppSession.IsAllowProgressiveBonus";

		public const string REQUEST_USERNAME = "Contants.AppSession.RequestUsername";
		public const string REQUEST_ROLEID = "Contants.AppSession.RequestRoleId";
		public const string IS_INTERNAL_AGENT = "Contants.AppSession.IsInternalAgent";
		public const string FROM_SYSTEM = "Contants.AppSession.FromSystem";
		public const string SUB_ACCOUNT_ID = "Contants.AppSession.SubAccountId";
		public const string LANGUAGE = "Contants.AppSession.Language";
    }
}