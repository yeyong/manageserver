using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// SellerCat Data Structure.
    /// </summary>
    [Serializable]
    public class SellerCat : BaseObject
    {
        [XmlElement("cid")]
        public string Cid { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_cid")]
        public string ParentCid { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("sort_order")]
        public int SortOrder { get; set; }
    }
}
