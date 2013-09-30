// -----------------------------------------------------------------------
// <copyright file="CustomerSettingSection.cs" company="Nexcel">
// @Steven.Nguyen
// </copyright>
// -----------------------------------------------------------------------

using System.Configuration;

namespace Fanex.CustomerSetting.Security.Configurations
{
    using System;

    /// <summary>
    /// Customer setting section.
    /// </summary>
    public class CustomerSettingSection : ConfigurationSection
    {
        private static CustomerSettingSection _instance;

        [ConfigurationProperty("authenticateFactory", IsRequired = false)]
        public string AuthenticateFactory
        {
            get
            {
                return (string)base["authenticateFactory"];
            }
        }

        [ConfigurationProperty("allowDirectDownline", IsRequired = false)]
        public bool AllowDirectDownline
        {
            get
            {
                return (bool)base["allowDirectDownline"];
            }
        }

        [ConfigurationProperty("allowGroups", IsRequired = false)]
        public string AllowGroups
        {
            get
            {
                return (string)base["allowGroups"];
            }
        }

        /// <summary>
        /// Customer Setting Section instance
        /// </summary>
        public static CustomerSettingSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = (CustomerSettingSection)ConfigurationManager.GetSection("CustomerSetting/Security");
                    }
                    catch (Exception e)
                    {
                        throw new ConfigurationErrorsException("Error when parsing configuration section CustomerSetting.", e);
                    }
                }

                return _instance;
            }
        }
    }
}
