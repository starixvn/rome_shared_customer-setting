// --------------------------------------------------------------------------
// <copyright file="RoleIdConstants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Constants
{
    /// <summary>
    /// Role Identify Constants.
    /// </summary>
    public static class RoleIdConstants
    {
        public const int Member = 1;

        public const int Agent = 2;

        public const int Master = 3;

        public const int Super = 4;

        /// <summary>
        /// Gets the role name.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The role name.</returns>
        public static string RoleName(int roleId)
        {
            switch (roleId)
            {
                case Super:
                    return "Super";
                case Master:
                    return "Master";
                case Agent:
                    return "Agent";
                default:
                    return "Member";
            }
        }
    }
}
