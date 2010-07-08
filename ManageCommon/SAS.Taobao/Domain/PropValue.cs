using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// PropValue Data Structure.
    /// </summary>
    [Serializable]
    public class PropValue : BaseObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("is_parent")]
        public bool IsParent { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("name_alias")]
        public string NameAlias { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlElement("prop_name")]
        public string PropName { get; set; }

        [XmlElement("sort_order")]
        public int SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("vid")]
        public long Vid { get; set; }
    }
}
