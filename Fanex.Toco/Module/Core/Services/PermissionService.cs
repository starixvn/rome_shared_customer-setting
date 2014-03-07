// --------------------------------------------------------------------------
// <copyright file="PermissionService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Refactoring.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Services.Interfaces;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Model.ValueObject;

    /// <summary>
    /// The PermissionService class.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private Dictionary<string, int> _roleAccess;

        public PermissionService()
        {
            _roleAccess = AppSession.RoleAccess;
        }

        public PermissionService(Dictionary<string, int> roleAccess)
        {
            _roleAccess = roleAccess ?? new Dictionary<string, int>();
        }

        /// <summary>
        /// Get permission to access with each menu item of current user login.
        /// </summary>
        /// <param name="currentFunctions">The current functions.</param>
        /// <returns>The dictionary of roleAccess.</returns>
        public Dictionary<string, int> GetRoleAccess(IEnumerable<FunctionPolicy> currentFunctions)
        {
            var roleAccesses = new Dictionary<string, int>();

            foreach (FunctionPolicy functionPolicy in currentFunctions)
            {
                if (functionPolicy.Policy >= (int)Constant.Policy.Read)
                {
                    roleAccesses.Add(functionPolicy.Function.Key, functionPolicy.Policy);
                }
            }

            return roleAccesses;
        }

        /// <summary>
        /// Check that user has permission to access one menu link.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Whether has read permission or not.</returns>
        public bool HasReadPermission(string functionName)
        {
            return this.HasReadPermission(_roleAccess, functionName);
        }

        /// <summary>
        /// Determines whether [has host permission] [the specified function name].
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Whether has host permission or not.</returns>
        public bool HasHostPermission(string functionName)
        {
            return this.HasHostPermission(_roleAccess, functionName);
        }

        /// <summary>
        /// Determines whether the specified function name has permission.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Whether has permission or not.</returns>
        public bool HasPermission(string functionName)
        {
            return this.HasPermission(_roleAccess, functionName);
        }

        /// <summary>
        /// Check that user has permission to access one menu link.
        /// </summary>
        /// <param name="roleAccess">The role access.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// Whether has read permission or not.
        /// </returns>
        internal bool HasReadPermission(Dictionary<string, int> roleAccess, string functionName)
        {
            return roleAccess != null &&
                   roleAccess.ContainsKey(functionName) &&
                   roleAccess[functionName] == (int)Constant.Policy.Read;
        }

        /// <summary>
        /// Determines whether [has host permission] [the specified function name].
        /// </summary>
        /// <param name="roleAccess">The role access.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// Whether has host permission or not.
        /// </returns>
        internal bool HasHostPermission(Dictionary<string, int> roleAccess, string functionName)
        {
            return roleAccess != null &&
                   roleAccess.ContainsKey(functionName) &&
                   roleAccess[functionName] == (int)Constant.Policy.Host;
        }

        /// <summary>
        /// Determines whether the specified function name has permission.
        /// </summary>
        /// <param name="roleAccess">The role access.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// Whether has permission or not.
        /// </returns>
        internal bool HasPermission(Dictionary<string, int> roleAccess, string functionName)
        {
            return roleAccess != null &&
                   roleAccess.ContainsKey(functionName) &&
                   roleAccess[functionName] >= (int)Constant.Policy.Read;
        }
    }
}
