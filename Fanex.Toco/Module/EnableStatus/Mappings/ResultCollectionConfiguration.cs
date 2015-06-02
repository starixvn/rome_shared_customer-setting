// --------------------------------------------------------------------------
// <copyright file="ResultCollectionConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    [ConfigurationCollection(typeof(ResultElementConfiguration), AddItemName = "column")]
    public class ResultCollectionConfiguration : ConfigurationElementCollection, IEnumerable<ResultElementConfiguration>
    {
        public ResultElementConfiguration this[int index]
        {
            get
            {
                return BaseGet(index) as ResultElementConfiguration;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ResultElementConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var configElement = element as ResultElementConfiguration;
            return configElement.Name;
        }

        IEnumerator<ResultElementConfiguration> IEnumerable<ResultElementConfiguration>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
