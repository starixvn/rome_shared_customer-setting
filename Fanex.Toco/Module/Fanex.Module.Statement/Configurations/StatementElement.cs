// --------------------------------------------------------------------------
// <copyright file="StatementElement.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Configurations
{
    using System;
    using System.Configuration;
    using Fanex.Common;

    /// <summary>
    /// Statement Element.
    /// </summary>
    public class StatementElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
        [ConfigurationProperty("newlayoutdate", IsRequired = true)]
        public DateTime NewLayoutDate
        {
            get
            {
                return Converter.ToDateTime(this["newlayoutdate"]);
            }

            set
            {
                this["newlayoutdate"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
        [ConfigurationProperty("showCreatorOnDetail", IsRequired = false, DefaultValue = false)]
        public bool ShowCreatorOnDetail
        {
            get
            {
                return Converter.ToBool(this["showCreatorOnDetail"]);
            }

            set
            {
                this["showCreatorOnDetail"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
        [ConfigurationProperty("showExchangeRateOnDetail", IsRequired = false, DefaultValue = false)]
        public bool ShowExchangeRateOnDetail
        {
            get
            {
                return Converter.ToBool(this["showExchangeRateOnDetail"]);
            }

            set
            {
                this["showExchangeRateOnDetail"] = value;
            }
        }
    }
}
