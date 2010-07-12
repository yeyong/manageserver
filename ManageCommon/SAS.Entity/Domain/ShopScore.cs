using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ShopScore Data Structure.
    /// </summary>
    [Serializable]
    public class ShopScore : BaseObject
    {
        [XmlElement("delivery_score")]
        public string DeliveryScore { get; set; }

        [XmlElement("item_score")]
        public string ItemScore { get; set; }

        [XmlElement("service_score")]
        public string ServiceScore { get; set; }
    }
}
