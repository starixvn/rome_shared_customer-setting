// -----------------------------------------------------------------------
// <copyright file="Writer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Module.Logs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Writer
    {
        public static IList<string> GetLogs(Formater.LogType type = Formater.LogType.Info)
        {
            return new List<string>();
        }

        public static void Write(this Exception ex, int custId = 0)
        {
        }  
    }
}
