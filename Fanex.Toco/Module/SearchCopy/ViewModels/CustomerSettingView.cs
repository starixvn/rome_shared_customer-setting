// --------------------------------------------------------------------------
// <copyright file="CustomerSettingView.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.11.04">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.ViewModels
{
    using System;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class CustomerSettingView
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public HideSports StatusSports { get; set; }
    }
}
