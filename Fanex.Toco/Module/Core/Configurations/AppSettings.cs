// --------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Steven.Nguyen" date="2012.10.12">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Configurations
{
    using System.Configuration;

    /// <summary>
    /// The AppSettings class.
    /// </summary>
    public static class AppSettings
    {
        static AppSettings()
        {
            IsLocalTest = ConfigurationManager.AppSettings["IsLocalTest"] == "1";
        }

        public static bool IsLocalTest { get; private set; }
    }
}