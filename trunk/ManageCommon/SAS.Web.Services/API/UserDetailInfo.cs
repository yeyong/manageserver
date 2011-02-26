using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    /// <summary>
    /// 团队成员详细信息
    /// </summary>
    public class MemberInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonPropertyAttribute("memberid")]
        [XmlElement("memberid")]
        public int Memberid;

        /// <summary>
        /// 用户名称（星名称英文）
        /// </summary>
        [JsonPropertyAttribute("name")]
        [XmlElement("name")]
        public string Name = string.Empty;

        /// <summary>
        /// 用户昵称（星名）
        /// </summary>
        [JsonPropertyAttribute("nickname")]
        [XmlElement("nickname")]
        public string Nickname = string.Empty;

        /// <summary>
        /// 职务
        /// </summary>
        [JsonPropertyAttribute("job")]
        [XmlElement("job")]
        public string Job = string.Empty;

        /// <summary>
        /// 星等级
        /// </summary>
        [JsonPropertyAttribute("level")]
        [XmlElement("level")]
        public int Level;

        /// <summary>
        /// 星亮度
        /// </summary>
        [JsonPropertyAttribute("light")]
        [XmlElement("light")]
        public int Light;

        /// <summary>
        /// QQ
        /// </summary>
        [JsonPropertyAttribute("qq")]
        [XmlElement("qq")]
        public string QQ = string.Empty;

        /// <summary>
        /// MSN
        /// </summary>
        [JsonPropertyAttribute("msn")]
        [XmlElement("msn")]
        public string MSN = string.Empty;

        /// <summary>
        /// EMail
        /// </summary>
        [JsonPropertyAttribute("email")]
        [XmlElement("email")]
        public string EMail = string.Empty;

        /// <summary>
        /// 个人主页
        /// </summary>
        [JsonPropertyAttribute("home")]
        [XmlElement("home")]
        public string Home = string.Empty;

        /// <summary>
        /// 身材
        /// </summary>
        [JsonPropertyAttribute("figure")]
        [XmlElement("figure")]
        public string Figure = string.Empty;

        /// <summary>
        /// 团队年龄
        /// </summary>
        [JsonPropertyAttribute("teamAge")]
        [XmlElement("teamAge")]
        public int TeamAge;

        /// <summary>
        /// 生日
        /// </summary>
        [JsonPropertyAttribute("birthday")]
        [XmlElement("birthday")]
        public string Birthday = string.Empty;

        /// <summary>
        /// 来自
        /// </summary>
        [JsonPropertyAttribute("location")]
        [XmlElement("location")]
        public string Location = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        [JsonPropertyAttribute("gender")]
        [XmlElement("gender")]
        public string Gender = string.Empty;

        /// <summary>
        /// 生肖
        /// </summary>
        [JsonPropertyAttribute("sx")]
        [XmlElement("sx")]
        public string Sx = string.Empty;

        /// <summary>
        /// 星座
        /// </summary>
        [JsonPropertyAttribute("xz")]
        [XmlElement("xz")]
        public string Xz = string.Empty;

        /// <summary>
        /// 个人简介
        /// </summary>
        [JsonPropertyAttribute("bio")]
        [XmlElement("bio")]
        public string Bio = string.Empty;

        /// <summary>
        /// 学历
        /// </summary>
        [JsonPropertyAttribute("edu")]
        [XmlElement("edu")]
        public string Edu = string.Empty;

        /// <summary>
        /// 专业
        /// </summary>
        [JsonPropertyAttribute("profession")]
        [XmlElement("profession")]
        public string Profession = string.Empty;

        /// <summary>
        /// 特长
        /// </summary>
        [JsonPropertyAttribute("specially")]
        [XmlElement("specially")]
        public string Specially = string.Empty;

        /// <summary>
        /// 爱好
        /// </summary>
        [JsonPropertyAttribute("hobby")]
        [XmlElement("hobby")]
        public string Hobby = string.Empty;

        /// <summary>
        /// 形象照片地址
        /// </summary>
        [JsonPropertyAttribute("img")]
        [XmlElement("img")]
        public string Img = string.Empty;

        /// <summary>
        /// 形象照片背景地址
        /// </summary>
        [JsonPropertyAttribute("imgbak")]
        [XmlElement("imgbak")]
        public string Imgbak = string.Empty;

        /// <summary>
        /// 形象照片列表地址
        /// </summary>
        [JsonPropertyAttribute("imglist")]
        [XmlElement("imglist")]
        public string Imglist = string.Empty;

        /// <summary>
        /// 星系意义
        /// </summary>
        [JsonPropertyAttribute("sign")]
        [XmlElement("sign")]
        public string Sign = string.Empty;

        /// <summary>
        /// 自我描述
        /// </summary>
        [JsonPropertyAttribute("selfdesc")]
        [XmlElement("selfdesc")]
        public string Selfdesc = string.Empty;

        /// <summary>
        /// 自我畅享
        /// </summary>
        [JsonPropertyAttribute("selfenjoy")]
        [XmlElement("selfenjoy")]
        public string Selfenjoy = string.Empty;
    }
}
