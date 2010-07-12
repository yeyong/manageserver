using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// LogisticsCompany Data Structure.
    /// </summary>
    [Serializable]
    public class LogisticsCompany : BaseObject
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
