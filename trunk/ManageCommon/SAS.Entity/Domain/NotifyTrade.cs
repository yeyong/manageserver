using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// NotifyTrade Data Structure.
    /// </summary>
    [Serializable]
    public class NotifyTrade : BaseObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("is_3D")]
        public bool Is3D { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}
