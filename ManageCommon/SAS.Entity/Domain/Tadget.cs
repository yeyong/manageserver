using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Tadget Data Structure.
    /// </summary>
    [Serializable]
    public class Tadget : BaseObject
    {
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        [XmlElement("app_secret")]
        public string AppSecret { get; set; }
    }
}
