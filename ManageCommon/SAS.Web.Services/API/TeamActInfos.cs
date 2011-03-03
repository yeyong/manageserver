using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 团队信息实体
    /// </summary>
    public class TeamInfos
    {
        /// <summary>
        /// 团队ID
        /// </summary>
        [JsonPropertyAttribute("teamid")]
        [XmlElement("teamid")]
        public int TeamID;

        /// <summary>
        /// 团队名称
        /// </summary>
        [JsonPropertyAttribute("name")]
        [XmlElement("name")]
        public string Name = string.Empty;

        /// <summary>
        /// 团队域名
        /// </summary>
        [JsonPropertyAttribute("domain")]
        [XmlElement("domain")]
        public string Domain = string.Empty;

        /// <summary>
        /// 模板id
        /// </summary>
        [JsonPropertyAttribute("tempid")]
        [XmlElement("tempid")]
        public int TempID;

        /// <summary>
        /// 成立时间
        /// </summary>
        [JsonPropertyAttribute("builddate")]
        [XmlElement("builddate")]
        public string Builddate = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonPropertyAttribute("createdate")]
        [XmlElement("createdate")]
        public string Createdate = string.Empty;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonPropertyAttribute("lastdate")]
        [XmlElement("lastdate")]
        public string Lastdate = string.Empty;
        /// <summary>
        /// 团队图片地址
        /// </summary>
        [JsonPropertyAttribute("img")]
        [XmlElement("img")]
        public string Img = string.Empty;
        /// <summary>
        /// 团队简述
        /// </summary>
        [JsonPropertyAttribute("bio")]
        [XmlElement("bio")]
        public string Bio = string.Empty;
        /// <summary>
        /// 团队意义
        /// </summary>
        [JsonPropertyAttribute("sign")]
        [XmlElement("sign")]
        public string Sign = string.Empty;
        /// <summary>
        /// 团队工作方向和工作内容
        /// </summary>
        [JsonPropertyAttribute("teamwork")]
        [XmlElement("teamwork")]
        public string Teamwork = string.Empty;
        /// <summary>
        /// 人员职责和基本构成
        /// </summary>
        [JsonPropertyAttribute("constitute")]
        [XmlElement("constitute")]
        public string Constitute = string.Empty;
        /// <summary>
        /// 团队状态（默认1，正常；0，停用）
        /// </summary>
        [JsonPropertyAttribute("stutas")]
        [XmlElement("stutas")]
        public int Stutas;
        /// <summary>
        /// 查看次数
        /// </summary>
        [JsonPropertyAttribute("views")]
        [XmlElement("views")]
        public int Views;
        /// <summary>
        /// 显示顺序
        /// </summary>
        [JsonPropertyAttribute("order")]
        [XmlElement("order")]
        public int Order;
        /// <summary>
        /// 成员名（逗号分割）
        /// </summary>
        [JsonPropertyAttribute("members")]
        [XmlElement("members")]
        public string Members = string.Empty;
        /// <summary>
        /// 搜索优化关键字
        /// </summary>
        [JsonPropertyAttribute("seokey")]
        [XmlElement("seokey")]
        public string Seokey = string.Empty;
        /// <summary>
        /// 搜索优化描述信息
        /// </summary>
        [JsonPropertyAttribute("seodesc")]
        [XmlElement("seodesc")]
        public string Seodesc = string.Empty;
        /// <summary>
        /// 创建人（可以更改）拥有修改团队信息权限
        /// </summary>
        [JsonPropertyAttribute("creater")]
        [XmlElement("creater")]
        public string Creater = string.Empty;
    }

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
