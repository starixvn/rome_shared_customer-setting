// <copyright file="LogType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Entities
{
    /// <summary>
    /// The LogType enumeration.
    /// </summary>
    public enum LogType : int
    {
        /// <summary>
        /// The none item.
        /// </summary>
        None = 0,

        /// <summary>
        /// The information item.
        /// </summary>
        Info = 1,

        /// <summary>
        /// The error item.
        /// </summary>
        Error = 2
    }
}