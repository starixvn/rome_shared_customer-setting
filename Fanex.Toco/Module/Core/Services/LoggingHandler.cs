// <copyright file="LoggingHandler.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services
{
    using System;
    using Fanex.CustomerSetting.Module.Core.Entities;
    using Fanex.CustomerSetting.Module.Core.Services.Interfaces;
    using Fanex.Logging;

    /// <summary>
    /// The Logging Handler.
    /// </summary>
    public class LoggingHandler : ILoggingHandler
    {
        private Action<string> _infoLog;
        private Action<string> _errorLog;
        private Action<string, Exception> _errorLogAccompanyException;

        public LoggingHandler()
        {
            _infoLog = LogService.Info;
            _errorLog = LogService.Error;
            _errorLogAccompanyException = LogService.Error;
        }

        internal LoggingHandler(Action<string> infoLog, Action<string> errorLog, Action<string, Exception> errorLogAccompanyException)
        {
            _infoLog = infoLog;
            _errorLog = errorLog;
            _errorLogAccompanyException = errorLogAccompanyException;
        }

        public void Log(Exception exception)
        {
            _errorLogAccompanyException(exception.Message, exception);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logType">Type of the log.</param>
        public void Log(string message = "", LogType logType = LogType.Error)
        {
            switch (logType)
            {
                case LogType.Info:
                    _infoLog(message);
                    break;
                case LogType.Error:
                    _errorLog(message);
                    break;
            }
        }
    }
}
