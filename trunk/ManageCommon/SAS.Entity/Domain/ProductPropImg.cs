using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ProductPropImg Data Structure.
    /// </summary>
    [Serializable]
    public class ProductPropImg : BaseObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("position")]
        public int Position { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}
