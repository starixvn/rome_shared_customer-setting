using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Fanex.CustomerSetting.Module.ColossusBets.Configurations
{
    public class ColossusBetsSection : ConfigurationSection
    {
        private static IDictionary<string, string> _colossusBetsConfiguration;

        private static IDictionary<string, string> ColossusBetsConfiguration
        {
            get
            {
                _colossusBetsConfiguration = _colossusBetsConfiguration ??
                    Instance.Configurations
                    .Cast<ColossusBetsElement>()
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                return _colossusBetsConfiguration;
            }
        }

        public static ColossusBetsSection Instance
        {
            get
            {
                return ConfigurationManager.GetSection("ColossusBetsSection") as ColossusBetsSection;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ColossusBetsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ColossusBetsCollection Configurations
        {
            get
            {
                return (ColossusBetsCollection)base["Configurations"];
            }
        }

        public static string SiteId
        {
            get
            {
                return GetValue("SiteId");
            }
        }

        public static string TokenUrl
        {
            get
            {
                return GetValue("ColossusBetsAPI_GetToken");
            }
        }

        public static string MemberChoice
        {
            get
            {
                return GetValue("ColossusBetsAPI_MemberChoice");
            }
        }

        public static string GetDomainBySiteId(string siteId)
        {
            return GetValue(siteId);
        }

        private static string GetValue(string key)
        {
            string value;

            if (!ColossusBetsConfiguration.TryGetValue(key, out value))
            {
                value = string.Empty;
            }

            return value;
        }
    }
}
