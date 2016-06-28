// --------------------------------------------------------------------------
// <copyright file="LogCollection.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.10.21">Created</change>
// </history>

namespace Fanex.Module.Logs.Mappings
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ConfigurationCollection(typeof(LogElement), AddItemName = "Log")]
    public class LogCollection : ConfigurationElementCollection, IEnumerable<LogElement>
    {
        [ConfigurationProperty("SensitiveParams", IsRequired = false)]
        public string SensitiveParams
        {
            get
            {
                return base["SensitiveParams"] as string;
            }
        }

        [ConfigurationProperty("SensitiveReplacement", IsRequired = false)]
        public string SensitiveReplacement
        {
            get
            {
                return base["SensitiveReplacement"] as string;
            }
        }

        public LogElement this[int index]
        {
            get
            {
                return BaseGet(index) as LogElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LogElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogElement)element).Type;
        }

        IEnumerator<LogElement> IEnumerable<LogElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
