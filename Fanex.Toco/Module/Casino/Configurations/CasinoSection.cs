// -----------------------------------------------------------------------
// <copyright file="KenoSection.cs" company="">
// Steven.Nguyen
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Casino.Configurations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Fanex.CustomerSetting.Module.Core;
    using System;
    /// <summary>
    /// Keno config section.
    /// </summary>
    public class CasinoSection : ConfigurationSection
    {
        private static CasinoSection _instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Error when parsing configuration section CustomerSetting - Casino.</exception>
        public static CasinoSection Instance
        {
            get
            {
                if (_instance == null)
                {
                      _instance = (CasinoSection)ConfigurationManager.GetSection("CustomerSetting/Casino");
                }

                return _instance;
            }
        }

        [ConfigurationProperty("UMTime", DefaultValue = false, IsRequired = false)]
        public bool UMTime
        {
            get
            {      
                return (bool)base["UMTime"];
            }
        }

        [ConfigurationProperty("From",  IsRequired = false)]
        public string From
        {
            get
            {
                return (string)base["From"];
            }
        }

        [ConfigurationProperty("To",  IsRequired = false)]
        public string To
        {
            get
            {
                return (string)base["To"];
            }
        }
    }
}