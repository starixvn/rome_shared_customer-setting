// --------------------------------------------------------------------------
// <copyright file="BetType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class BetType : BaseModel
    {
        public int Type { get; set; }

        public bool LiveIndicator { get; set; }
    }
}
