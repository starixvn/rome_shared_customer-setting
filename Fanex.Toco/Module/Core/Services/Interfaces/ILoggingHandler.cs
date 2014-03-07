// <copyright file="ILoggingHandler.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services.Interfaces
{
    using System;
    using Fanex.CustomerSetting.Module.Core.Entities;

    /// <summary>
    /// The ILoggingHandler interface.
    /// </summary>
    public interface ILoggingHandler
    {
        void Log(Exception exception);

        void Log(string message, LogType logType);
    }
}
