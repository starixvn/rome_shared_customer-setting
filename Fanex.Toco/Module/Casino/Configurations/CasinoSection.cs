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

        [ConfigurationProperty("IsUMTime", DefaultValue = false, IsRequired = false)]
        public bool IsUMTime
        {
            get
            {      
                return (bool)base["IsUMTime"];
            }
        }

        [ConfigurationProperty("UMFrom",  IsRequired = false)]
        public string UMFrom
        {
            get
            {
                return (string)base["UMFrom"];
            }
        }

        [ConfigurationProperty("UMTo",  IsRequired = false)]
        public string UMTo
        {
            get
            {
                return (string)base["UMTo"];
            }
        }
    }
}