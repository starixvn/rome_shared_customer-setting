// --------------------------------------------------------------------------
// <copyright file="CustomerInformation.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
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

        public string Fax { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }

        public bool IsDisable { get; set; }

        public bool HideKeno { get; set; }

        public int UserId { get; set; }
    }
}