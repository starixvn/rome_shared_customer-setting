// -----------------------------------------------------------------------
// <copyright file="KenoSection.cs" company="Nexcel">
// Steven.Nguyen
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.KenoStatus.Configurations
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Keno config section.
    /// </summary>
    public class KenoSection : ConfigurationSection
    {
        private static KenoSection _instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Error when parsing configuration section CustomerSetting - Keno.</exception>
        public static KenoSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = (KenoSection)ConfigurationManager.GetSection("CustomerSetting/Keno");
                    }
                    catch (Exception e)
                    {
                        throw new ConfigurationErrorsException("Error when parsing configuration section CustomerSetting - Keno.", e);
                    }
                }

                return _instance;
            }
        }

        [ConfigurationProperty("enable", IsRequired = false)]
        public bool Enable
        {
            get
            {
                return (bool)base["enable"];
            }
        }

        [ConfigurationProperty("showsetting", IsRequired = false)]
        public bool ShowSetting
        {
            get
            {
                return (bool)base["showsetting"];
            }
        }
    }
}