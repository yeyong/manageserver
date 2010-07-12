using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ShippingAddress Data Structure.
    /// </summary>
    [Serializable]
    public class ShippingAddress : BaseObject
    {
        [XmlElement("address_id")]
        public int AddressId { get; set; }

        [XmlElement("is_default")]
        public bool IsDefault { get; set; }

        [XmlElement("location")]
        public Location Location { get; set; }

        [XmlElement("mobile")]
        public string Mobile { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }
    }
}
