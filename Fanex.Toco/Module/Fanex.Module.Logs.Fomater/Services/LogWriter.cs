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
    using System.Collections;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Web;
    using Fanex.Logging;
    using Fanex.Module.Logs.Mappings;
    using Sunplus.UMControllerLibrary;

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

        private static string UmURL
        {
            get
            {
                return _configuration.Logs.UmURL;
            }
        }

        private static Sites Site
        {
            get
            {
                return _configuration.Logs.Site;
            }
        }

        public static void Write(this Exception ex)
        {
            var um = new UMBusiness();
            var isUM = um.isUnderMaintainence(Site, UmURL);

            if (isUM)
            {
                return;
            }

            var log = GetLog(ex.Message);

            if (log != null)
            {
                switch (log.Type)
                {
                    case Formater.LogType.Info:
                        LogService.Info(ex.FormatLog());
                        break;
                    case Formater.LogType.Warning:
                        LogService.Warn(ex.FormatLog());
                        break;
                    case Formater.LogType.Critical:
                        LogService.Critical(ex.FormatLog());
                        break;
                    default:
                        LogService.Error(ex.FormatLog());
                        break;
                }
            }
            else
            {
                LogService.Error(ex.FormatLog());
            }
        }
        
        private static LogElement GetLog(string message)
        {
            var logs = _configuration.Logs;

            foreach (LogElement log in logs)
            {
                foreach (MessageElement msg in log.Messages)
                {
                    if (!string.IsNullOrWhiteSpace(msg.Text) && message.Contains(msg.Text))
                    {
                        return log;
                    }                        
                }
            }

            return default(LogElement);
        }

        private static string ExceptionDataToString(IDictionary dictionary)
        {
            var message = string.Empty;

            foreach (var key in dictionary.Keys)
            {
                message += string.Format("\r\n{0}: {1}", key, dictionary[key]);
            }

            return message;
        }

        private static string FormatLog(this Exception ex)
        {
            var format = string.Join(
                "\r\n", 
                "{0}",
                "URL: {1}",
                "SOURCE: {2}",
                "DOMAIN: {3}",
                "REFERRER: {4}",
                "FORM: {5}",
                "QUERYSTRING: {6}",
                "TARGETSITE: {7}",
                "DETAIL: {8}",
                "STACKTRACE: {9}",
                "CULTURE: {10}",
                "DATA: {11}",
                "SQL: {12}",
                "BROWSER: {13}");

            var request = HttpContext.Current.Request;

            return string.Format(
                format, 
                ex.Message,
                ex.FormatSensitiveInfo(request.Url.OriginalString),
                ex.Source,
                request.Headers["Host"],
                request.UrlReferrer,
                ex.FormatSensitiveInfo(request.Form.ToString()),
                ex.FormatSensitiveInfo(request.QueryString.ToString()),
                ex.TargetSite,
                ex,
                ex.StackTrace,
                CultureInfo.CurrentCulture.Name,
                ExceptionDataToString(ex.Data),
                ex.FormatSqlException(),
                ClientInformation(request));
        }

        private static string FormatSqlException(this Exception ex)
        {
            var sqlEx = ex as SqlException;
            if (sqlEx != null)
            {
                var message = string.Join(
                    "\r\n",
                    "PROCEDURE: {0}",
                    "LINENUMBER: {1}",
                    "SERVER: {2}",
                    "ERRORCODE: {3}");

                return string.Format(message, sqlEx.Procedure, sqlEx.LineNumber, sqlEx.Server, sqlEx.ErrorCode);
            }

            return string.Empty;
        }

        private static string FormatSensitiveInfo(this Exception ex, string parameterString)
        {
            var sensitiveStrings = SensitiveParams.Split(',');

            if (string.IsNullOrEmpty(parameterString) || sensitiveStrings.Length == 0)
            {
                return parameterString;
            }

            var splitChar = "&";
            var paramsArray = parameterString.Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);

            int length;
            if ((length = paramsArray.Length) == 0)
            {
                return parameterString;
            }

            for (int i = 0; i < length; i++)
            {
                var parameter = paramsArray[i].ToLower();
                foreach (string sensitive in sensitiveStrings)
                {
                    if (parameter.Contains(sensitive))
                    {
                        var indexOf = parameter.IndexOf('=');
                        if (indexOf >= 0)
                        {
                            paramsArray[i] = paramsArray[i].Substring(0, indexOf + 1) + SensitiveReplacement;
                        }

                        break;
                    }
                }
            }

            return string.Join(splitChar, paramsArray);
        }

        private static string ClientInformation(HttpRequest request)
        {
            if (request.Browser == null)
            {
                return string.Empty;
            }

            var browser = request.Browser;
            var browserInformation = string.Join(
                "\r\n",
                "Type: " + browser.Type,
                "Name: " + browser.Browser,
                "Version: " + browser.Version,
                "Major Version: " + browser.MajorVersion,
                "Minor Version: " + browser.MinorVersion,
                "Platform: " + browser.Platform,
                "Is Beta: " + browser.Beta,
                "Is Crawler: " + browser.Crawler,
                "Is AOL: " + browser.AOL,
                "Is Win16: " + browser.Win16,
                "Is Win32: " + browser.Win32,
                "Supports Frames: " + browser.Frames,
                "Supports Tables: " + browser.Tables,
                "Supports Cookies: " + browser.Cookies);

            return browserInformation;
        }
    }
}