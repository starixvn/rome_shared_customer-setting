// --------------------------------------------------------------------------
// <copyright file="StatementSection.cs" company="Nexcel Solutions Vietnam">
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
    /// Statement Section.
    /// </summary>
    public class StatementSection : ConfigurationSection
    {
        private static StatementSection _instance;

        /// <summary>
        /// Gets Progressive Instance.
        /// </summary>
        /// <value>The instance of the progressive section.</value>
        public static StatementSection Instance
        {
            get
            {
                _instance = _instance ?? ConfigurationManager.GetSection("StatementSection") as StatementSection;
                return _instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item is enabled.
        /// </summary>
        /// <value>True or false.</value>
        [ConfigurationPropertyAttribute("newlayoutdate", IsRequired = true)]
        public DateTime NewLayoutDate
        {
            get
            {
                return Converter.ToDateTime(this["newlayoutdate"]);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
        [ConfigurationProperty("systemShowCreatorOnDetail", IsRequired = false, DefaultValue = "")]
        public string SystemShowCreatorOnDetail
        {
            get
            {
                return Converter.ToString2(this["systemShowCreatorOnDetail"]);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
        [ConfigurationProperty("systemShowExchangeRateOnDetail", IsRequired = false, DefaultValue = "")]
        public string SystemShowExchangeRateOnDetail
        {
            get
            {
                return Converter.ToString2(this["systemShowExchangeRateOnDetail"]);
            }
        }
    }
}
