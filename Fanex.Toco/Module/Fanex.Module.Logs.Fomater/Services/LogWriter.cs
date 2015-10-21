// --------------------------------------------------------------------------
// <copyright file="LogWriter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.10.21">Created</change>
// </history>

namespace Fanex.Module.Logs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Logging;
    using Fanex.Module.Logs.Mappings;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class LogWriter
    {
        private static LogsConfiguration _configuration;

        static LogWriter()
        {
            _configuration = _configuration ?? LogsConfiguration.Instance;
        }

        private static string SensitiveParams
        {
            get
            {
                return _configuration.Logs.SensitiveParams;
            }
        }

        private static string SensitiveReplacement
        {
            get
            {
                return _configuration.Logs.SensitiveReplacement;
            }
        }

        public static void Write(this Exception ex, int custId = 0)
        {
            var log = GetLogs(ex.Message);

            if (log != null)
            {
                switch (log.Type)
                {
                    case Formater.LogType.Info:
                        LogService.Info(FormatLog(ex.Message, custId), ex);
                        break;
                    case Formater.LogType.Warning:
                        LogService.Warn(FormatLog(ex.Message, custId), ex);
                        break;
                    case Formater.LogType.Critical:
                        LogService.Critical(FormatLog(ex.Message, custId), ex);
                        break;
                    default:
                        LogService.Error(FormatLog(ex.Message, custId), ex);
                        break;
                }
            }
            else
            {
                LogService.Error(FormatLog(ex.Message, custId), ex);
            }
        }

        private static IEnumerable<LogElement> GetLogs(Formater.LogType type = Formater.LogType.Info)
        {
            var logs = _configuration.Logs;

            return logs.Where(log => log.Type == type);
        }

        private static LogElement GetLogs(string message)
        {
            var logs = _configuration.Logs;

            foreach (LogElement log in logs)
            {
                foreach (MessageElement msg in log.Messages)
                {
                    if (message.Contains(msg.Text))
                    {
                        return log;
                    }                        
                }
            }

            return default(LogElement);
        }

        private static string FormatLog(string log, int custId = 0)
        {
            // TODO: Will implement later
            return string.Empty;
        }
    }
}