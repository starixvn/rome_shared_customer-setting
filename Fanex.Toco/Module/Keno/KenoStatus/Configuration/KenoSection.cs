// -----------------------------------------------------------------------
// <copyright file="KenoSection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.CustomerSetting.Module.KenoStatus.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// Keno section, define dynamic configure from web.config.
    /// </summary>
    public class KenoSection:ConfigurationSection
    {
        private static KenoSection _instance;

        [ConfigurationProperty("enableKeno", IsRequired = false)]
        public bool EnableKeno
        {
            get
            {
                return (bool)base["enableKeno"];
            }
        }
        
        /// <summary>
        /// Keno section instance
        /// </summary>
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
    }
}
