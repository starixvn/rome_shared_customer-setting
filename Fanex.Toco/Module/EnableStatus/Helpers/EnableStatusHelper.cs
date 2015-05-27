// --------------------------------------------------------------------------
// <copyright file="EnableStatusHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers
{
    using Fanex.CustomerSetting.Module.Core.Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class EnableStatusHelper
    {
        public static string GetResource(string key)
        {
            return ResourceExtension.GetResource("Fanex.CustomerSetting.EnableStatus.AllInOne.Resources.EnableStatusResources", key);
        }
    }
}
