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
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpElementConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("params", IsRequired = false)]
        public ParamCollectionConfiguration ParamCollection
        {
            get
            {
                return base["params"] as ParamCollectionConfiguration;
            }
        }

        [ConfigurationProperty("results", IsRequired = false)]
        public ResultCollectionConfiguration ResultCollection
        {
            get
            {
                return base["results"] as ResultCollectionConfiguration;
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
        public StoredProcedure.SpType Type
        {
            get
            {
                return (StoredProcedure.SpType)base["type"];
            }

            set
            {
                base["type"] = value;
            }
        }
    }
}
