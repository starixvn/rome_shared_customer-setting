// --------------------------------------------------------------------------
// <copyright file="MessageCollection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    [ConfigurationCollection(typeof(MessageElement), AddItemName = "Message")]
    public class MessageCollection : ConfigurationElementCollection, IEnumerable<MessageElement>
    {
        public MessageElement this[int index]
        {
            get
            {
                return BaseGet(index) as MessageElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MessageElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MessageElement)element).Key;
        }

        IEnumerator<MessageElement> IEnumerable<MessageElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
