// --------------------------------------------------------------------------
// <copyright file="EnableStatusEntity.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities
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
        private bool _hasDownline = true;

        public bool IsDirty { get; set; }

        public bool Hide { get; set; }

        public bool HasDownline
        {
            get
            {
                return _hasDownline;
            }

            set
            {
                _hasDownline = value;
            }
        }

        public object SettingType { get; set; }

        public int ProductId { get; set; }

        public int MessageType { get; set; }

        public object Username { get; set; }

        public object SportTypeId { get; set; }

        public object BetTypeId { get; set; }

        public object SportTypeName { get; set; }

        public object BetTypeName { get; set; }

        public object Disabled { get; set; }

        public object DisabledXML { get; set; }

        public object Synced { get; set; }

        public object DisabledUplines { get; set; }

        public object DisabledDownlines { get; set; }

        public object EnableText { get; set; }

        public object EnableDownlinesText { get; set; }
    }
}