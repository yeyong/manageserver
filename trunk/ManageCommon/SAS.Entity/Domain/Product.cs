using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Product Data Structure.
    /// </summary>
    [Serializable]
    public class Product : BaseObject
    {
        [XmlElement("binds")]
        public string Binds { get; set; }

        [XmlElement("binds_str")]
        public string BindsStr { get; set; }

        [XmlElement("cat_name")]
        public string CatName { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlArray("product_imgs")]
        [XmlArrayItem("product_img")]
        public List<ProductImg> ProductImgs { get; set; }

        [XmlArray("product_prop_imgs")]
        [XmlArrayItem("product_prop_img")]
        public List<ProductPropImg> ProductPropImgs { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("props_str")]
        public string PropsStr { get; set; }

        [XmlElement("sale_props")]
        public string SaleProps { get; set; }

        [XmlElement("sale_props_str")]
        public string SalePropsStr { get; set; }

        [XmlElement("tsc")]
        public string Tsc { get; set; }
    }
}
