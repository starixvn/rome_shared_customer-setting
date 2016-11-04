namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings
{
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpElementConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("params", IsRequired = false)]
        public ParamCollectionConfiguration ParamCollection
        {
            get
            {
                return base["params"] as ParamCollectionConfiguration;
            }
        }

        [ConfigurationProperty("results", IsRequired = false)]
        public ResultCollectionConfiguration ResultCollection
        {
            get
            {
                return base["results"] as ResultCollectionConfiguration;
            }
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get 
            {
                return base["key"] as string;
            }

            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public StoredProcedure.SpType Type
        {
            get
            {
                return (StoredProcedure.SpType)base["type"];
            }

            set
            {
                base["type"] = value;
            }
        }
    }
}
