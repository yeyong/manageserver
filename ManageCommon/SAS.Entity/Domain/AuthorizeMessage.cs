using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// AuthorizeMessage Data Structure.
    /// </summary>
    [Serializable]
    public class AuthorizeMessage : BaseObject
    {
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("end_date")]
        public string EndDate { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("start_date")]
        public string StartDate { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}
