// --------------------------------------------------------------------------
// <copyright file="ParamCollectionConfiguration.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ConfigurationCollection(typeof(ParamElementConfiguration), AddItemName = "param")]
    public class ParamCollectionConfiguration : ConfigurationElementCollection, IEnumerable<ParamElementConfiguration>
    {
        public ParamElementConfiguration this[int index]
        {
            get
            {
                return BaseGet(index) as ParamElementConfiguration;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParamElementConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var configElement = element as ParamElementConfiguration;
            return configElement.Name;
        }
        
        IEnumerator<ParamElementConfiguration> IEnumerable<ParamElementConfiguration>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
