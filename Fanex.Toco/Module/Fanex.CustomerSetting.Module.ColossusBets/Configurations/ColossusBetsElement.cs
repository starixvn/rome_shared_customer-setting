using System.Configuration;

namespace Fanex.CustomerSetting.Module.ColossusBets.Configurations
{
    public class ColossusBetsElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true, IsRequired = true, DefaultValue = "")]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }

            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = true, DefaultValue = "")]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }

            set
            {
                this["value"] = value;
            }
        }
    }
}
