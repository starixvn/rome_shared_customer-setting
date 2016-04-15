namespace Fanex.Voidbridge.BetDetail.Configurations
{
    using System.Configuration;

    public class OWVoidbridgeCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public OWVoidbridgeElement this[int index]
        {
            get
            {
                return (OWVoidbridgeElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        public new OWVoidbridgeElement this[string elementKey]
        {
            get
            {
                return (OWVoidbridgeElement)BaseGet(elementKey);
            }
        }

        public void Add(OWVoidbridgeElement element)
        {
            BaseAdd(element);
        }

        public void Remove(OWVoidbridgeElement element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.Key);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string alias)
        {
            BaseRemove(alias);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(OWVoidbridgeElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement kenoBetTypesElement)
        {
            BaseAdd(kenoBetTypesElement, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new OWVoidbridgeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OWVoidbridgeElement)element).Key;
        }
    }
}
