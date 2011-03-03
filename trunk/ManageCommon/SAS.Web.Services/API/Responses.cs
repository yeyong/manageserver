using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SAS.Entity;

namespace SAS.Web.Services.API
{
    #region auth
    [XmlRoot("auth_createToken_response", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class TokenInfo
    {
        [XmlElement("session_key")]
        public string Token;
    }

    [XmlRoot("auth_getSession_response", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class SessionInfo
    {
        [XmlElement("session_key")]
        public string SessionKey;

        [XmlElement("uid")]
        public long UId;

        [XmlElement("user_name")]
        public string UserName;

        [XmlElement("expires")]
        public long Expires;

        //[XmlIgnore ()]
        //public bool IsInfinite
        //{
        //    get { return Expires == 0; }
        //}	

        public SessionInfo()
        { }

        // use this if you want to create a session based on infinite session
        // credentials
        public SessionInfo(string session_key, long uid)
        {
            this.SessionKey = session_key;
            this.UId = uid;
            this.Expires = 0;
        }
    }

    [XmlRoot("auth_register_response", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class RegisterResponse
    {
        [XmlText]
        public int Uid;

        //[XmlAttribute("list")]
        //public bool List;
    }

    [XmlRoot("auth_encodePassword_response", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class EncodePasswordResponse
    {
        [XmlText]
        public string Password;
    }
    #endregion

    #region 企业信息操作
    [XmlRoot("company_getlist", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class CompanyGetListResponse
    {
        [XmlElement("cnums")]
        [JsonProperty("cnums")]
        public int Cnums;

        [XmlElement("companylist")]
        [JsonProperty("companylist")]
        public CompanyInfo[] CompanyList;
    }
    #endregion

    #region 专题信息操作
    [XmlRoot("activity_getlist", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class ActivityGetListResponse
    {
        [XmlElement("anums")]
        [JsonProperty("anums")]
        public int Anums;

        [XmlElement("activitylist")]
        [JsonProperty("activitylist")]
        public ActivityInfo[] ActivityList;
    }
    #endregion

    #region 团队成员信息操作

    [XmlRoot("teaminfo_get", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class TeamGetResponse
    {
        [XmlElement("teaminfo")]
        [JsonProperty("teaminfo")]
        public TeamInfos Team;
    }
    [XmlRoot("member_getlist", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class MemberGetListResponse
    {
        [XmlElement("mnums")]
        [JsonProperty("mnums")]
        public int Mnums;

        [XmlElement("memberlist")]
        [JsonProperty("memberlist")]
        public MemberInfo[] MemberList;
    }
    /// <summary>
    /// 获取团队活动信息
    /// </summary>
    [XmlRoot("TeamAct_getlist", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class TeamActGetListResponse
    {
        [XmlElement("actnums")]
        [JsonProperty("actnums")]
        public int Actnums;

        [XmlElement("teamactlist")]
        [JsonProperty("teamactlist")]
        public TeamActInfos[] TeamactList;
    }
    /// <summary>
    /// 获取团队成果信息
    /// </summary>
    [XmlRoot("TeamWork_getlist", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class TeamWorkGetListResponse
    {
        [XmlElement("wnums")]
        [JsonProperty("wnums")]
        public int Wnums;

        [XmlElement("teamworklist")]
        [JsonProperty("teamworklist")]
        public TeamWorkInfos[] TeamWorkList;
    }
    #endregion

    public class ArgResponse
    {
        [XmlElement("arg")]
        public Arg[] Args;

        [XmlAttribute("list")]
        public bool List;
    }
}
