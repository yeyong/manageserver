using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace SAS.Web.Services.API
{
    /// <summary>
    /// 淘之购商品信息
    /// </summary>
    public class TaoBaoGoodInfo
    {
        /// <summary>
        /// 商品序号
        /// </summary>
        [JsonPropertyAttribute("gid")]
        [XmlElement("gid")]
        public int Gid;
        /// <summary>
        /// 商品ID
        /// </summary>
        [JsonPropertyAttribute("gnumiid")]
        [XmlElement("gnumiid")]
        public string GNumiid = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonPropertyAttribute("gname")]
        [XmlElement("gname")]
        public string GName = string.Empty;
        /// <summary>
        /// 商品图片
        /// </summary>
        [JsonPropertyAttribute("gpic")]
        [XmlElement("gpic")]
        public string GPic = string.Empty;
        /// <summary>
        /// 商品类别ID
        /// </summary>
        [JsonPropertyAttribute("cid")]
        [XmlElement("cid")]
        public int Cid;
        /// <summary>
        /// 商品类别名称
        /// </summary>
        [JsonPropertyAttribute("cname")]
        [XmlElement("cname")]
        public string CName = string.Empty;
    }
}
