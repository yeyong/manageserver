using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Sku Data Structure.
    /// </summary>
    [Serializable]
    public class Sku : BaseObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("extra_id")]
        public long ExtraId { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}
