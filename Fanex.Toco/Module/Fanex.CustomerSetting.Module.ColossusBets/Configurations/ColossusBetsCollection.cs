namespace Fanex.CustomerSetting.Module.ColossusBets.Configurations
{
    using System.Configuration;

    public class ColossusBetsCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public ColossusBetsElement this[int index]
        {
            get
            {
                return (ColossusBetsElement)BaseGet(index);
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

        public new ColossusBetsElement this[string elementKey]
        {
            get
            {
                return (ColossusBetsElement)BaseGet(elementKey);
            }
        }

        public void Add(ColossusBetsElement element)
        {
            BaseAdd(element);
        }

        public void Remove(ColossusBetsElement element)
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

        public int IndexOf(ColossusBetsElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement kenoBetTypesElement)
        {
            BaseAdd(kenoBetTypesElement, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new ColossusBetsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ColossusBetsElement)element).Key;
        }
    }
}
