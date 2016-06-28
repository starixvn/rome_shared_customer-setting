// --------------------------------------------------------------------------
// <copyright file="EnableStatusConfiguration.cs" company="">
// Copyright (c). All rights reserved.
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
                _instance = _instance ?? ConfigurationManager.GetSection("enablestatus") as EnableStatusConfiguration;
                return _instance;
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