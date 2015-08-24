// --------------------------------------------------------------------------
// <copyright file="Roles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The Roles enumerate.
    /// </summary>
    public enum Roles : int
    {
        /// <summary>
        /// None item.
        /// </summary>
        None = 0,

        /// <summary>
        /// The member item.
        /// </summary>
        Member = 1,

        /// <summary>
        /// The agent item.
        /// </summary>
        Agent = 2,

        /// <summary>
        /// The master item.
        /// </summary>
        Master = 3,

        /// <summary>
        /// The super item.
        /// </summary>
        Super = 4
    }
}