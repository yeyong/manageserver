using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 浙商咨询信息API实体
    /// </summary>
    public class NewsInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyAttribute("nid")]
        [XmlElement("nid")]
        public int Nid;

        /// <summary>
        /// 新闻ID
        /// </summary>
        [JsonPropertyAttribute("newsID")]
        [XmlElement("newsID")]
        public string NewsID =  string.Empty;

        /// <summary>
        /// 资讯标题
        /// </summary>
        [JsonPropertyAttribute("newstitle")]
        [XmlElement("newstitle")]
        public string NewsTitle = string.Empty;

        /// <summary>
        /// 新闻Url
        /// </summary>
        [JsonPropertyAttribute("newsurl")]
        [XmlElement("newsurl")]
        public string NewsUrl = string.Empty;

        /// <summary>
        /// 新闻图片
        /// </summary>
        [JsonPropertyAttribute("newspic")]
        [XmlElement("newspic")]
        public string NewsPic = string.Empty;

    }
}
