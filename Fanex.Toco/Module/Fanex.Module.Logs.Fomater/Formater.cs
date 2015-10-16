// -----------------------------------------------------------------------
// <copyright file="Formater.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Module.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Formater
    {
        public enum LogType
        {            
            Info = 0,
            Warning = 1,
            Critical = 2
        }

        public enum SensitiveParams
        {
            Password = 0,
            SecurityCode = 1
        }
    }
}
