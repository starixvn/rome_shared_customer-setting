// --------------------------------------------------------------------------
// <copyright file="LogsConfiguration.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.10.21">Created</change>
// </history>

namespace Fanex.Module.Logs.Mappings
{
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogsConfiguration : ConfigurationSection
    {
        private static LogsConfiguration _instance;

        public static LogsConfiguration Instance
        {
            get 
            {
                _instance = _instance ?? ConfigurationManager.GetSection("LogsConfiguration") as LogsConfiguration;

                return _instance;
            }
        }

        [ConfigurationProperty("Logs", IsRequired = true)]
        public LogCollection Logs
        {
            get
            {
                return base["Logs"] as LogCollection;
            }
        }
    }
}
