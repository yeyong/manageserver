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

    public class ArgResponse
    {
        [XmlElement("arg")]
        public Arg[] Args;

        [XmlAttribute("list")]
        public bool List;
    }
}
