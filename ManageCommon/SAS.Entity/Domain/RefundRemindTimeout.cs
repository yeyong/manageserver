using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// RefundRemindTimeout Data Structure.
    /// </summary>
    [Serializable]
    public class RefundRemindTimeout : BaseObject
    {
        [XmlElement("exist_timeout")]
        public bool ExistTimeout { get; set; }

        [XmlElement("remind_type")]
        public int RemindType { get; set; }

        [XmlElement("timeout")]
        public string Timeout { get; set; }
    }
}
