// --------------------------------------------------------------------------
// <copyright file="ParamElementConfiguration.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings
{
    using System.Configuration;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParamElementConfiguration : ConfigurationElement
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

        [ConfigurationProperty("default", IsRequired = false)]
        public string Default
        {
            get
            {
                return base["default"] as string;
            }
        }
    }
}