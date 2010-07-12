using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// ItemProp Data Structure.
    /// </summary>
    [Serializable]
    public class ItemProp : BaseObject
    {
        [XmlElement("child_template")]
        public string ChildTemplate { get; set; }

        [XmlElement("is_allow_alias")]
        public bool IsAllowAlias { get; set; }

        [XmlElement("is_color_prop")]
        public bool IsColorProp { get; set; }

        [XmlElement("is_enum_prop")]
        public bool IsEnumProp { get; set; }

        [XmlElement("is_input_prop")]
        public bool IsInputProp { get; set; }

        [XmlElement("is_item_prop")]
        public bool IsItemProp { get; set; }

        [XmlElement("is_key_prop")]
        public bool IsKeyProp { get; set; }

        [XmlElement("is_sale_prop")]
        public bool IsSaleProp { get; set; }

        [XmlElement("multi")]
        public bool Multi { get; set; }

        [XmlElement("must")]
        public bool Must { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_pid")]
        public long ParentPid { get; set; }

        [XmlElement("parent_vid")]
        public long ParentVid { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlArray("prop_values")]
        [XmlArrayItem("prop_value")]
        public List<PropValue> PropValues { get; set; }

        [XmlElement("sort_order")]
        public int SortOrder { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}
