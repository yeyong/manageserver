using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 活动信息
    /// </summary>
    public class ActivityInfo
    {
        /// <summary>
        /// 活动专题ID
        /// </summary>
        [JsonPropertyAttribute("aid")]
        [XmlElement("aid")]
        public int Aid;

        /// <summary>
        /// 活动标题
        /// </summary>
        [JsonPropertyAttribute("Atitle")]
        [XmlElement("Atitle")]
        public string Atitle = string.Empty;
    }
}
