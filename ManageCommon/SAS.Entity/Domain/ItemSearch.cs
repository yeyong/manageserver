using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ItemSearch Data Structure.
    /// </summary>
    [Serializable]
    public class ItemSearch : BaseObject
    {
        [XmlArray("item_categories")]
        [XmlArrayItem("item_category")]
        public List<ItemCategory> ItemCategories { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<Item> Items { get; set; }
    }
}
