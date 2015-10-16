// -----------------------------------------------------------------------
// <copyright file="LogsConfiguration.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Module.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogsConfiguration
    {
        private static LogsConfiguration _instance;

        public static LogsConfiguration Instance
        {
            get 
            {
                _instance = _instance ?? ConfigurationManager.GetSection("Logs") as LogsConfiguration;

                return _instance;
            }
        }
    }
}
