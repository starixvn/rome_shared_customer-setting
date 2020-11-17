// --------------------------------------------------------------------------
// <copyright file="CustomerInformation.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

using Fanex.CustomerSetting.Module.Keno.KenoSetting.Configurations;
using System;
using System.Linq;

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The CustomerInformation entity.
    /// </summary>
    public class CustomerInformation
    {
        public int CustId { get; set; }

        public int UplineId { get; set; }

        public int RoleId { get; set; }

        public int ActionType { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public bool IsDisableAutoMPT { get; set; }

        public string Fax { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }

        public bool IsDisable { get; set; }

        public bool HideKeno { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public bool IsMultiple { get; set; }

        public string Site { get; set; }

        public bool IsPT3Decimal => AppSettings.PT3DecimalSites().Any(site => site.Equals(this.Site, StringComparison.CurrentCultureIgnoreCase));
    }
}