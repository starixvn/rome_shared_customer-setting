// -----------------------------------------------------------------------
// <copyright file="LogsConfiguration.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Module.Logs
{
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
                _instance = _instance ?? ConfigurationManager.GetSection("LogsConfiguration") as LogsConfiguration;

                return _instance;
            }
        }
    }
}
