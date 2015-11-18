namespace Fanex.CustomerSetting.Module.ColossusBets.Configurations
{
    using System.Configuration;
    using Fanex.Common;

    public class ColossusBetsElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true, IsRequired = true, DefaultValue = "")]
        public string Key
        {
            get
            {
                return Converter.ToString2(this["key"]);
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
                return Converter.ToString2(this["value"]);
            }

            set
            {
                this["value"] = value;
            }
        }
    }
}
