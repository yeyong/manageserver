using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// PicUrl Data Structure.
    /// </summary>
    [Serializable]
    public class PicUrl : BaseObject
    {
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
