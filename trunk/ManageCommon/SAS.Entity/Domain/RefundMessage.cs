using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// RefundMessage Data Structure.
    /// </summary>
    [Serializable]
    public class RefundMessage : BaseObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("message_type")]
        public string MessageType { get; set; }

        [XmlElement("owner_id")]
        public long OwnerId { get; set; }

        [XmlElement("owner_nick")]
        public string OwnerNick { get; set; }

        [XmlElement("owner_role")]
        public string OwnerRole { get; set; }

        [XmlArray("pic_urls")]
        [XmlArrayItem("pic_url")]
        public List<PicUrl> PicUrls { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }
    }
}
