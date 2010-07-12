using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// PostageMode Data Structure.
    /// </summary>
    [Serializable]
    public class PostageMode : BaseObject
    {
        [XmlElement("dests")]
        public string Dests { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("increase")]
        public string Increase { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}
