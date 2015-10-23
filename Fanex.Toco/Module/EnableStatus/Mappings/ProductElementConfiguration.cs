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

        [ConfigurationProperty("hascheckall", IsRequired = false)]
        public bool HasCheckAll
        {
            get
            {
                return Utils.ToBoolean(base["hascheckall"]);
            }
        }

        [ConfigurationProperty("reloadafterupdate", IsRequired = false)]
        public bool ReloadAfterUpdate
        {
            get
            {
                return Utils.ToBoolean(base["reloadafterupdate"]);
            }
        }

        [ConfigurationProperty("newmode", IsRequired = false)]
        public bool NewMode
        {
            get
            {
                return Utils.ToBoolean(base["newmode"]);
            }
        }

        [ConfigurationProperty("hidebettypes", IsRequired = false)]
        public string HideBetTypes
        {
            get
            {
                return base["hidebettypes"] as string;
            }
        }

        [ConfigurationProperty("warning", IsRequired = false)]
        public string Warning
        {
            get
            {
                return base["warning"] as string;
            }
        }

        [ConfigurationProperty("function", IsRequired = true)]
        public string Function
        {
            get
            {
                return base["function"] as string;
            }
        }

        [ConfigurationProperty("closepopupjs", IsRequired = false)]
        public string ClosePopupFunction
        {
            get
            {
                return base["closepopupjs"] as string;
            }
        }        
    }
}