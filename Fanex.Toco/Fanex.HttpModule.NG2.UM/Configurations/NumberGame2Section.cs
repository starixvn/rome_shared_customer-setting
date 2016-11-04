using System.Configuration;

namespace Fanex.HttpModule.NG2.UM.Configurations
{
    public class NumberGame2Section : ConfigurationSection
    {
        private static NumberGame2Section _numberGame2Session = null;

        [ConfigurationProperty("WebUrl", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(NumberGame2Collection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public NumberGame2Collection WebUrl
        {
            get { return (NumberGame2Collection)base["WebUrl"]; }
        }

        [ConfigurationProperty("UMPageUrl", IsRequired = true)]
        public string UMPageUrl
        {
            get { return (string)this["UMPageUrl"]; }
        }

        public static NumberGame2Section Instance
        {
            get
            {
                _numberGame2Session = _numberGame2Session ?? ConfigurationManager.GetSection("NumberGame2Section") as NumberGame2Section;
                return _numberGame2Session;
            }
        }

        public NumberGame2Collection this[NumberGame2Collection name]
        {
            get
            {
                var numberGame2Collection =
                    (NumberGame2Collection)
                    this.GetType().GetProperty(name.ToString()).GetValue(this, System.Reflection.BindingFlags.GetField,
                                                                         null, null, null);
                return numberGame2Collection;
            }
        }
    }

    public class NumberGame2Collection : ConfigurationElementCollection
    {
        public NumberGame2Collection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(NumberGame2Element settingElement)
        {
            BaseAdd(settingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(NumberGame2Element settingElement)
        {
            if (BaseIndexOf(settingElement) >= 0)
                BaseRemove(settingElement.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

        public NumberGame2Element this[int index]
        {
            get
            {
                return (NumberGame2Element)BaseGet(index);
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

        new public NumberGame2Element this[string Name]
        {
            get
            {
                return (NumberGame2Element)BaseGet(Name);
            }
        }

        public int IndexOf(NumberGame2Element settingElement)
        {
            return BaseIndexOf(settingElement);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new NumberGame2Element();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NumberGame2Element)element).Name;
        }
    }

    public class NumberGame2Element : ConfigurationElement
    {
        public NumberGame2Element()
        {
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("filter", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["filter"] ?? string.Empty;
            }
            set
            {
                this["filter"] = value;
            }
        }

        [ConfigurationProperty("isajax", IsRequired = false, DefaultValue = false)]
        public bool IsAjax
        {
            get
            {
                return (bool)this["isajax"];
            }
            set
            {
                this["isajax"] = value;
            }
        }
    }
}