namespace Fanex.Module.RNGCasino.Configurations
{
    using System.Configuration;

    public class OWRNGCasinoCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public OWRNGCasinoElement this[int index]
        {
            get
            {
                return (OWRNGCasinoElement)BaseGet(index);
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

        public new OWRNGCasinoElement this[string elementKey]
        {
            get
            {
                return (OWRNGCasinoElement)BaseGet(elementKey);
            }
        }

        public void Add(OWRNGCasinoElement element)
        {
            BaseAdd(element);
        }

        public void Remove(OWRNGCasinoElement element)
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

        public int IndexOf(OWRNGCasinoElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement kenoBetTypesElement)
        {
            BaseAdd(kenoBetTypesElement, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new OWRNGCasinoElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OWRNGCasinoElement)element).Key;
        }
    }
}
