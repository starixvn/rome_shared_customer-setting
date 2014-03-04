// --------------------------------------------------------------------------
// <copyright file="IKenoSession.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    /// <summary>
    /// Bound the app session from core.
    /// </summary>
    public interface IKenoSession
    {
        int UserId { get; }

        int SubAccId { get; }

        string Ip { get; }
    }
}