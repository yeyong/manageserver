using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// NotifyItem Data Structure.
    /// </summary>
    [Serializable]
    public class NotifyItem : BaseObject
    {
        [XmlElement("changed_fields")]
        public string ChangedFields { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num")]
        public int Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}
