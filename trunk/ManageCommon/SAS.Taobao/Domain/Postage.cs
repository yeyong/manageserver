using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// Postage Data Structure.
    /// </summary>
    [Serializable]
    public class Postage : BaseObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("ems_increase")]
        public string EmsIncrease { get; set; }

        [XmlElement("ems_price")]
        public string EmsPrice { get; set; }

        [XmlElement("express_increase")]
        public string ExpressIncrease { get; set; }

        [XmlElement("express_price")]
        public string ExpressPrice { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("post_increase")]
        public string PostIncrease { get; set; }

        [XmlElement("post_price")]
        public string PostPrice { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlArray("postage_modes")]
        [XmlArrayItem("postage_mode")]
        public List<PostageMode> PostageModes { get; set; }
    }
}
