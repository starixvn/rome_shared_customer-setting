// <copyright file="IPermissionService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Core.Model.ValueObject;

    /// <summary>
    /// The IPermissionService interface.
    /// </summary>
    public interface IPermissionService
    {
        Dictionary<string, int> GetRoleAccess(IEnumerable<FunctionPolicy> currentFunctions);

        bool HasReadPermission(string functionName);

        bool HasHostPermission(string functionName);

        bool HasPermission(string functionName);
    }
}
