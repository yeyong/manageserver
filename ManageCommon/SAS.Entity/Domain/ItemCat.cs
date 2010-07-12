using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ItemCat Data Structure.
    /// </summary>
    [Serializable]
    public class ItemCat : BaseObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("is_parent")]
        public bool IsParent { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_cid")]
        public long ParentCid { get; set; }

        [XmlElement("sort_order")]
        public int SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}
