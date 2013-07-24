using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Configurations
{
    public class ProgressiveSection : ConfigurationSection
    {
        public static ProgressiveSection Instance
        {
            get
            {
                var Progressive = ConfigurationManager.GetSection("ProgressiveSection") as ProgressiveSection;
                return Progressive;
            }
        }

		[ConfigurationPropertyAttribute("active", IsRequired = true)]
		public bool Active
		{
			get
			{
				return (bool)this["active"];
			}
		}

		[ConfigurationPropertyAttribute("system", IsRequired = true)]
		public int System
		{
			get
			{
				return (int)this["system"];
			}
		}

        [ConfigurationProperty("Product", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ProgressiveCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ProgressiveCollection Product
        {
            get
            {
                var spCollection =
                    (ProgressiveCollection)base["Product"];
                return spCollection;
            }
        }
    }

    public class ProgressiveCollection : ConfigurationElementCollection
    {
        public ProgressiveCollection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(ProgressiveElement ProgressiveElement)
        {
            BaseAdd(ProgressiveElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(ProgressiveElement ProgressiveElement)
        {
            if (BaseIndexOf(ProgressiveElement) >= 0)
                BaseRemove(ProgressiveElement.Id);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string id)
        {
            BaseRemove(id);
        }

        public void Clear()
        {
            BaseClear();
        }

        public ProgressiveElement this[int index]
        {
            get
            {
                return (ProgressiveElement)BaseGet(index);
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

        new public ProgressiveElement this[string Alias]
        {
            get
            {
                return (ProgressiveElement)BaseGet(Alias);
            }
        }

        public int IndexOf(ProgressiveElement ProgressiveElement)
        {
            return BaseIndexOf(ProgressiveElement);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new ProgressiveElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProgressiveElement)element).Id;
        }
    }

    public class ProgressiveElement : ConfigurationElement
    {

        public ProgressiveElement()
        {

        }

        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public int Id
        {
            get
            {
                return (int)this["id"];
            }
            set
            {
                this["id"] = value;
            }
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

        [ConfigurationProperty("active", IsRequired = true)]
        public bool Active
        {
            get
            {
                return (bool)this["active"];
            }
            set
            {
                this["active"] = value;
            }
        }


        [ConfigurationProperty("stores", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ProgressiveCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ProgressiveCollection Stores
        {
            get
            {
                var stores =
                    (ProgressiveCollection)base["stores"];
                return stores;
            }
        }
    }
}
