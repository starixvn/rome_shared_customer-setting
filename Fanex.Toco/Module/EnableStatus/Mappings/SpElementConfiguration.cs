// --------------------------------------------------------------------------
// <copyright file="SpElementConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings
{
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpElementConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("params", IsRequired = true)]
        public ParamCollectionConfiguration ParamCollection
        {
            get
            {
                return base["params"] as ParamCollectionConfiguration;
            }
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get 
            {
                return base["key"] as string;
            }

            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return base["type"] as string;
            }

            set
            {
                base["type"] = value;
            }
        }
    }
}
