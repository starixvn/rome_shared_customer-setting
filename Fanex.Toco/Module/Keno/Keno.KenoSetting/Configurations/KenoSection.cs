// -----------------------------------------------------------------------
// <copyright file="KenoSection.cs" company="">
// Steven.Nguyen
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Configurations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Fanex.CustomerSetting.Module.Core;

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
                      _instance = (KenoSection)ConfigurationManager.GetSection("CustomerSetting/Keno");
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

        [ConfigurationProperty("exclude", IsRequired = false)]
        public string ExcludeBetTypes
        {
            get
            {
                return (string)base["exclude"];
            }
        }

        [ConfigurationProperty("excludesetting", IsRequired = false)]
        public string ExcludeSettingBetTypes
        {
            get
            {
                return (string)base["excludesetting"];
            }
        }
                
        public IList<int> ExcludeBetTypeList
        {
            get
            {
                var excludes = this.ExcludeBetTypes.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                var results = new List<int>();
                excludes.ToList().ForEach(o => 
                {
                    results.Add(Utils.ToInt32(o));
                });

                return results;
            }
        }

        public IList<int> ExcludeSettingBetTypeList
        {
            get
            {
                var excludes = this.ExcludeSettingBetTypes.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                var results = new List<int>();
                excludes.ToList().ForEach(o =>
                {
                    results.Add(Utils.ToInt32(o));
                });

                return results;
            }
        }
    }
}