// --------------------------------------------------------------------------
// <copyright file="EnableStatusConfiguration.cs" company="Nexcel Solutions Vietnam">
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
    public class EnableStatusConfiguration : ConfigurationSection
    {
        private static EnableStatusConfiguration _instance = null;

        public static EnableStatusConfiguration Instance
        {
            get
            {
                return _instance ?? ConfigurationManager.GetSection("enablestatus") as EnableStatusConfiguration;
            }
        }

        [ConfigurationProperty("products", IsRequired = true)]
        public ProductCollectionConfiguration ProductCollection
        {
            get
            {
                return base["products"] as ProductCollectionConfiguration;
            }
        }        
    }
}