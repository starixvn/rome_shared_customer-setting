using System;
using System.Web;

namespace Fanex.CustomerSetting.Module.NG2.Core.Extensions
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogExtension
    {
        public static void Log(Exception ex)
        {
            Fanex.Logging.Logger.Log.Error(GenerateLogMessage(ex));
        }

        public static void LogInfo(Exception ex)
        {
            Fanex.Logging.Logger.Log.Info(GenerateLogMessage(ex));
        }

        private static string GenerateLogMessage(Exception ex)
        {
            return ex.Message +
                       "\r\nSOURCE: " + ex.Source +
                       "\r\nDOMAIN: " + HttpContext.Current.Request.Headers["Host"] +
                        "\r\nREFERRER: " + HttpContext.Current.Request.UrlReferrer +
                       "\r\nFORM: " + HttpContext.Current.Request.Form.ToString() +
                       "\r\nQUERYSTRING: " + HttpContext.Current.Request.QueryString.ToString() +
                       "\r\nTARGETSITE: " + ex.TargetSite +
                       "\r\nSTACKTRACE: " + ex.StackTrace;
        }
    }
}
