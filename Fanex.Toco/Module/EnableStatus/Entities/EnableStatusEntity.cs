// --------------------------------------------------------------------------
// <copyright file="EnableStatusEntity.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusEntity
    {
        public bool Hide { get; set; }

        public object BetTypeId { get; set; }

        public object BetTypeName { get; set; }

        public object Disabled { get; set; }

        public object DisabledUplines { get; set; }

        public object DisabledDownlines { get; set; }

        public object DisabledText { get; set; }

        public object DisabledDownlinesText { get; set; }
    }
}