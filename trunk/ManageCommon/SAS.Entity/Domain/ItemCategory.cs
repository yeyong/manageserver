using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ItemCategory Data Structure.
    /// </summary>
    [Serializable]
    public class ItemCategory : BaseObject
    {
        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }
    }
}
