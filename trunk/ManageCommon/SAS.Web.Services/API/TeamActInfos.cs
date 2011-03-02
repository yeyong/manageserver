using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 团队成果实体
    /// </summary>
    public class TeamWorkInfos
    {
        /// <summary>
        /// 成果ID
        /// </summary>
        [JsonPropertyAttribute("actid")]
        [XmlElement("actid")]
        public int Actid;

        /// <summary>
        /// 成果名称
        /// </summary>
        [JsonPropertyAttribute("name")]
        [XmlElement("name")]
        public string Name = string.Empty;

        /// <summary>
        /// 成果发起时间
        /// </summary>
        [JsonPropertyAttribute("start")]
        [XmlElement("start")]
        public string Start = string.Empty;

        /// <summary>
        /// 成果结束时间
        /// </summary>
        [JsonPropertyAttribute("end")]
        [XmlElement("end")]
        public string End = string.Empty;

        /// <summary>
        /// 成果列表图
        /// </summary>
        [JsonPropertyAttribute("listpic")]
        [XmlElement("listpic")]
        public string Listpic = string.Empty;

        /// <summary>
        /// 成果列表背景图
        /// </summary>
        [JsonPropertyAttribute("picbak")]
        [XmlElement("picbak")]
        public string Picbak = string.Empty;

        /// <summary>
        /// 成果描述
        /// </summary>
        [JsonPropertyAttribute("desc")]
        [XmlElement("desc")]
        public string Desc = string.Empty;

        /// <summary>
        /// 成果链接
        /// </summary>
        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url = string.Empty;

        /// <summary>
        /// 成果参与成员
        /// </summary>
        [JsonPropertyAttribute("members")]
        [XmlElement("members")]
        public string Members = string.Empty;

        /// <summary>
        /// 团队ID
        /// </summary>
        [JsonPropertyAttribute("teamid")]
        [XmlElement("teamid")]
        public int Teamid;
    }

    /// <summary>
    /// 团队活动详细信息
    /// </summary>
    public class TeamActInfos
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonPropertyAttribute("actid")]
        [XmlElement("actid")]
        public int Actid;

        /// <summary>
        /// 活动名称
        /// </summary>
        [JsonPropertyAttribute("name")]
        [XmlElement("name")]
        public string Name = string.Empty;

        /// <summary>
        /// 活动发起时间
        /// </summary>
        [JsonPropertyAttribute("start")]
        [XmlElement("start")]
        public string Start = string.Empty;

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [JsonPropertyAttribute("end")]
        [XmlElement("end")]
        public string End = string.Empty;

        /// <summary>
        /// 活动简述
        /// </summary>
        [JsonPropertyAttribute("shortdesc")]
        [XmlElement("shortdesc")]
        public string Shortdesc = string.Empty;

        /// <summary>
        /// 活动列表图
        /// </summary>
        [JsonPropertyAttribute("listpic")]
        [XmlElement("listpic")]
        public string Listpic = string.Empty;

        /// <summary>
        /// 活动列表背景图
        /// </summary>
        [JsonPropertyAttribute("picbak")]
        [XmlElement("picbak")]
        public string Picbak = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        [JsonPropertyAttribute("atype")]
        [XmlElement("atype")]
        public int Atype;

        /// <summary>
        /// 图片集合
        /// </summary>
        [JsonPropertyAttribute("piccollect")]
        [XmlElement("piccollect")]
        public string Piccollect = string.Empty;

        /// <summary>
        /// 团队ID
        /// </summary>
        [JsonPropertyAttribute("teamid")]
        [XmlElement("teamid")]
        public int Teamid;
    }
}
