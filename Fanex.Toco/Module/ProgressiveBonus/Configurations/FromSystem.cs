// -----------------------------------------------------------------------
// <copyright file="FromSystem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  FromSystem
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Configurations
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// From system
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Names of enums are based on platform documentation.")]
    public enum FromSystem
    {
        Agency = 2,
        Accounting = 3
    }
}