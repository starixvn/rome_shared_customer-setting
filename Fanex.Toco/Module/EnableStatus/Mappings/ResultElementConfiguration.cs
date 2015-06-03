// --------------------------------------------------------------------------
// <copyright file="ResultElementConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.29">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings
{
    using System.Configuration;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResultElementConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("mapproperty", IsRequired = true)]
        public string MapProperty
        {
            get
            {
                return base["mapproperty"] as string;
            }
        }

        [ConfigurationProperty("reverse", IsRequired = false)]
        public bool Reverse
        {
            get
            {
                return (bool)base["reverse"];
            }
        }
    }
}