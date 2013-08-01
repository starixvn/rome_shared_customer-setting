// -----------------------------------------------------------------------
// <copyright file="ProgressiveSection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  ProgressiveSection
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Configurations
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Progressive Section Configuration.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
    public class ProgressiveSection : ConfigurationSection
    {
        /// <summary>
        /// Gets Progressive Instance.
        /// </summary>
        /// <value>The instance of the progressive section.</value>
        public static ProgressiveSection Instance
        {
            get
            {
                var progressive = ConfigurationManager.GetSection("ProgressiveSection") as ProgressiveSection; 
                return progressive;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item is enabled.
        /// </summary>
        /// <value>True or false.</value>
        [ConfigurationPropertyAttribute("active", IsRequired = true)]
        public bool Active
        {
            get
            {
                return (bool)this["active"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item is accessed.
        /// </summary>
        /// <value>System value 3 (accounting) or 4 (agent).</value>
        [ConfigurationPropertyAttribute("system", IsRequired = true)]
        public int System
        {
            get
            {
                return (int)this["system"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item is product collection.
        /// </summary>
        /// <value>Return product collection.</value>
        [ConfigurationProperty("Product", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ProgressiveCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ProgressiveCollection Product
        {
            get
            {
                var spCollection = (ProgressiveCollection)base["Product"];
                return spCollection;
            }
        }
    }

    /// <summary>
    /// Progressive Collection.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed."),
    SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Reviewed.")]
    public class ProgressiveCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Collection type.
        /// </summary>
        /// <value>Return collection type.</value>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Add an element.
        /// </summary>
        /// <param name="progressiveElement">Progressive element.</param>
        /// <value>Void method, no return.</value>
        public void Add(ProgressiveElement progressiveElement)
        {
            this.BaseAdd(progressiveElement);
        }        

        /// <summary>
        /// Remove element.
        /// </summary>
        /// <param name="progressiveElement">Progressive element.</param>
        public void Remove(ProgressiveElement progressiveElement)
        {
            if (this.BaseIndexOf(progressiveElement) >= 0)
            {
                this.BaseRemove(progressiveElement.Id);
            }
        }

        /// <summary>
        /// Remove at index.
        /// </summary>
        /// <param name="index">Index of a collection.</param>
        public void RemoveAt(int index)
        {
            this.BaseRemoveAt(index);
        }

        /// <summary>
        /// Remove by Id.
        /// </summary>
        /// <param name="id">Id of a collection.</param>
        public void Remove(string id)
        {
            this.BaseRemove(id);
        }

        /// <summary>
        /// Clear all item in collection.
        /// </summary>
        public void Clear()
        {
            this.BaseClear();
        }

        /// <summary>
        /// Gets a value indicating whether the item is product element.
        /// </summary>
        /// <param name="index">Index of a collection.</param>
        /// <returns>Return progressive element.</returns>
        public ProgressiveElement this[int index]
        {
            get
            {
                return (ProgressiveElement)BaseGet(index);
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item is product element.
        /// </summary>
        /// <param name="alias">Alias name.</param>
        /// <returns>Progressive element.</returns>
        public new ProgressiveElement this[string alias]
        {
            get
            {
                return (ProgressiveElement)BaseGet(alias); 
            }
        }

        /// <summary>
        /// Index of progressive element.
        /// </summary>
        /// <param name="progressiveElement">Progressive element value.</param>
        /// <returns>Return progressive element.</returns>
        public int IndexOf(ProgressiveElement progressiveElement)
        {
            return this.BaseIndexOf(progressiveElement);
        }

        /// <summary>
        /// Create new element.
        /// </summary>
        /// <returns>New element.</returns>
        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new ProgressiveElement();
        }

        /// <summary>
        /// Get element Id.
        /// </summary>
        /// <param name="element">Element value.</param>
        /// <returns>Return id.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProgressiveElement)element).Id;
        }

        /// <summary>
        /// Add an element.
        /// </summary>
        /// <param name="element">Configuration element.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }
    }

    /// <summary>
    /// Progressive element.
    /// </summary>
    public class ProgressiveElement : ConfigurationElement
    {       
        /// <summary>
        /// Gets or sets the name of the Id. 
        /// </summary>
        /// <value>The Id of the progressive element.</value>
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

        /// <summary>
        /// Gets or sets the name of the Name. 
        /// </summary>
        /// <value>The Name of the progressive element.</value>
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

        /// <summary>
        /// Gets or sets a value indicating whether the item is Active.
        /// </summary>
        /// <value>The active of the progressive element.</value>
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

        /// <summary>
        /// Gets a value indicating whether the item is stores collection.
        /// </summary>
        /// <value>The stores of the progressive element.</value>
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