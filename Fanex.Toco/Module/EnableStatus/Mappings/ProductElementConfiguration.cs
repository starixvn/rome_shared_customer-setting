// --------------------------------------------------------------------------
// <copyright file="ProductElementConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings
{
    using System.Configuration;
    using Fanex.CustomerSetting.Module.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductElementConfiguration : ConfigurationElement 
    {
        [ConfigurationProperty("sps", IsRequired = true)]
        public SpCollectionConfiguration SpCollection
        {
            get
            {
                return base["sps"] as SpCollectionConfiguration;
            }
        }

        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get
            {
                return base["id"] as string;
            }
        }

        [ConfigurationProperty("title", IsRequired = true)]
        public string Title
        {
            get
            {
                return base["title"] as string;
            }
        }

        [ConfigurationProperty("hascheckall", IsRequired = true)]
        public bool HasCheckAll
        {
            get
            {
                return Utils.ToBoolean(base["hascheckall"]);
            }
        }

        [ConfigurationProperty("width", IsRequired = true)]
        public string Width
        {
            get
            {
                return base["width"] as string;
            }
        }

        [ConfigurationProperty("height", IsRequired = true)]
        public string Height
        {
            get
            {
                return base["height"] as string;
            }
        }
        
        [ConfigurationProperty("showinframe", IsRequired = true)]
        public bool ShowInFrame
        {
            get
            {
                return Utils.ToBoolean(base["showinframe"]);
            }
        }

        [ConfigurationProperty("hidebettypes", IsRequired = true)]
        public string HideBetTypes
        {
            get
            {
                return base["hidebettypes"] as string;
            }
        }

        [ConfigurationProperty("warning", IsRequired = true)]
        public string Warning
        {
            get
            {
                return base["warning"] as string;
            }
        }
    }
}