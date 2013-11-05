// --------------------------------------------------------------------------
// <copyright file="IGeoIP.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.11.5">Created</change>
// </history>

namespace Fanex.ExternalAccount.IP.Geographic
{
    using Fanex.ExternalAccount.IP.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IGeoIP
    {
        IPTable LocationFromIP(string ip);
    }
}
