using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// SellerAuthorize Data Structure.
    /// </summary>
    [Serializable]
    public class SellerAuthorize : BaseObject
    {
        [XmlArray("brands")]
        [XmlArrayItem("brand")]
        public List<Brand> Brands { get; set; }

        [XmlArray("item_cats")]
        [XmlArrayItem("item_cat")]
        public List<ItemCat> ItemCats { get; set; }
    }
}
