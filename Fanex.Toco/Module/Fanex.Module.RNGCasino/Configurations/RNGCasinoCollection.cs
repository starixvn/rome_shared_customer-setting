namespace Fanex.Module.RNGCasino.Configurations
{
    using System.Configuration;

    public class RNGCasinoCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public RNGCasinoElement this[int index]
        {
            get
            {
                return (RNGCasinoElement)BaseGet(index);
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

        public new RNGCasinoElement this[string elementKey]
        {
            get
            {
                return (RNGCasinoElement)BaseGet(elementKey);
            }
        }

        public void Add(RNGCasinoElement element)
        {
            BaseAdd(element);
        }

        public void Remove(RNGCasinoElement element)
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

        public int IndexOf(RNGCasinoElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement kenoBetTypesElement)
        {
            BaseAdd(kenoBetTypesElement, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new RNGCasinoElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RNGCasinoElement)element).Key;
        }
    }
}
