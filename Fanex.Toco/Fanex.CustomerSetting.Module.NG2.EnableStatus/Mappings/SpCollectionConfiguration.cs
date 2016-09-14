namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ConfigurationCollection(typeof(SpElementConfiguration), AddItemName = "sp")]
    public class SpCollectionConfiguration : ConfigurationElementCollection, IEnumerable<SpElementConfiguration>
    {
        public SpElementConfiguration this[int index]
        {
            get
            {
                return BaseGet(index) as SpElementConfiguration;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SpElementConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var configElement = element as SpElementConfiguration;
            return configElement.Key;
        }

        IEnumerator<SpElementConfiguration> IEnumerable<SpElementConfiguration>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
