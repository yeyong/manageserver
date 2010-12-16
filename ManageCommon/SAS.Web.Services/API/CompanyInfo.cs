using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 企业信息
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        [JsonPropertyAttribute("enid")]
        [XmlElement("enid")]
        public int Enid;

        /// <summary>
        /// 企业名称
        /// </summary>
        [JsonPropertyAttribute("ename")]
        [XmlElement("ename")]
        public string Ename = string.Empty;

        /// <summary>
        /// 企业信息浏览量
        /// </summary>
        [JsonPropertyAttribute("enaccesses")]
        [XmlElement("enaccesses")]
        public int Enaccesses;

        /// <summary>
        /// 企业信誉度
        /// </summary>
        [JsonPropertyAttribute("encredits")]
        [XmlElement("encredits")]
        public int Encredits;

        /// <summary>
        /// 企业类别ID
        /// </summary>
        [JsonPropertyAttribute("encatalogid")]
        [XmlElement("encatalogid")]
        public int Encatalogid = 0;

        /// <summary>
        /// 企业类别名称
        /// </summary>
        [JsonPropertyAttribute("encatalogname")]
        [XmlElement("encatalogname")]
        public string Encatalogname = string.Empty;
    }
}
