namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings
{
    using System.Configuration;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParamElementConfiguration : ConfigurationElement
    {        
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("mapproperty", IsRequired = true)]
        public string MapProperty
        {
            get
            {
                return base["mapproperty"] as string;
            }
        }

        [ConfigurationProperty("reverse", IsRequired = false)]
        public bool Reverse
        {
            get
            {
                return (bool)base["reverse"];
            }
        }

        [ConfigurationProperty("default", IsRequired = false)]
        public string Default
        {
            get
            {
                return base["default"] as string;
            }
        }
    }
}