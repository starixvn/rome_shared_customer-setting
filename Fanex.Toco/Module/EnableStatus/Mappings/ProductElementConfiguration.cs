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

            set
            {
                base["id"] = value;
            }
        }

        [ConfigurationProperty("title", IsRequired = true)]
        public string Title
        {
            get
            {
                return base["title"] as string;
            }

            set
            {
                base["title"] = value;
            }
        }
        
        [ConfigurationProperty("hasgroup", IsRequired = true)]
        public bool HasGroup
        {
            get
            {
                return Utils.ToBoolean(base["hasgroup"]);
            }

            set
            {
                base["hasgroup"] = value;
            }
        }

        [ConfigurationProperty("hascheckall", IsRequired = true)]
        public bool HasCheckAll
        {
            get
            {
                return Utils.ToBoolean(base["hascheckall"]);
            }

            set
            {
                base["hascheckall"] = value;
            }
        }

        [ConfigurationProperty("width", IsRequired = true)]
        public string Width
        {
            get
            {
                return base["width"] as string;
            }

            set
            {
                base["width"] = value;
            }
        }

        [ConfigurationProperty("height", IsRequired = true)]
        public string Height
        {
            get
            {
                return base["height"] as string;
            }

            set
            {
                base["height"] = value;
            }
        }
        
        [ConfigurationProperty("showinframe", IsRequired = true)]
        public bool ShowInFrame
        {
            get
            {
                return Utils.ToBoolean(base["showinframe"]);
            }

            set
            {
                base["showinframe"] = value;
            }
        }
    }
}
