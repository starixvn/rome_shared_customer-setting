// --------------------------------------------------------------------------
// <copyright file="ICurrentContext.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/18">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Web
{
    using System.Web;

    public interface ICurrentContext
    {
        HttpContext Context { get; }

        string Ip4Address { get; }

        string Ip6Address { get; }

        string DomainName { get; }

        string FullDomainName { get; }

        string CountryCode { get; }
    }
}