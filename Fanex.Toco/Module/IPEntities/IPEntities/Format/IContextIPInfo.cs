// --------------------------------------------------------------------------
// <copyright file="IContextIPInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.11.5">Created</change>
// </history>

namespace Fanex.CustomerSetting.IP.Format
{
    public interface IContextIPInfo
    {
        int AdminId { get; set; }

        bool IsAdminCanViewVNIP { get; set; }

        string DefaultNoIPVN { get; set; }
    }
}
