// --------------------------------------------------------------------------
// <copyright file="Roles.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
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