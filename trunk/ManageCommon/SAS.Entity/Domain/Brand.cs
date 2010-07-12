using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Brand Data Structure.
    /// </summary>
    [Serializable]
    public class Brand : BaseObject
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlElement("prop_name")]
        public string PropName { get; set; }

        [XmlElement("vid")]
        public long Vid { get; set; }
    }
}
