using System;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Plugin.PasswordMode;
using SAS.Config;

namespace SAS.Web.Services.API.Actions
{
    public class Auth : ActionBase
    {
        /// <summary>
        /// 为客户端创建令牌
        /// </summary>
        /// <returns></returns>
        public string CreateToken()
        {
            string returnStr = "";
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return returnStr;
            }

            //ApplicationInfo appInfo = null;
            //APIConfigInfo apiInfo = APIConfigs.GetConfig();

            //ApplicationInfoCollection appcollection = apiInfo.AppCollection;
            //foreach (ApplicationInfo newapp in appcollection)
            //{
            //    if (newapp.APIKey == GetParam("api_key").ToString())
            //    {
            //        appInfo = newapp;
            //    }
            //}
            //应用程序类型为Web的时候应用程序没有调用此方法的权限
            if (this.App.ApplicationType == (int)ApplicationType.WEB)
            {
                ErrorCode = (int)ErrorType.API_EC_PERMISSION_DENIED;
                return returnStr;
            }

            OnlineUserInfo oluserinfo = OnlineUsers.UpdateInfo(Config.Passwordkey, Config.Onlinetimeout);
            int olid = oluserinfo.Ol_id;

            string expires = string.Empty;
            DateTime expireUTCTime;
            TokenInfo token = new TokenInfo();

            if (System.Web.HttpContext.Current.Request.Cookies["sas"] == null || System.Web.HttpContext.Current.Request.Cookies["sas"]["expires"] == null)
            {
                token.Token = "";
                if (Format == FormatType.JSON)
                    returnStr = "";
                else
                    returnStr = SerializationHelper.Serialize(token);
                return returnStr;
            }
            expires = System.Web.HttpContext.Current.Request.Cookies["dnt"]["expires"].ToString();
            ShortUserInfo userinfo = SAS.Logic.Users.GetShortUserInfo(oluserinfo.Ol_ps_id);
            expireUTCTime = DateTime.Parse(userinfo.Ps_lastactivity).ToUniversalTime().AddSeconds(Convert.ToDouble(expires));
            expires = Utils.ConvertToUnixTimestamp(expireUTCTime).ToString();
            //CreateToken
            //OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
            string time = string.Empty;// = DateTime.Parse(OnlineUsers.GetOnlineUser(olid).Lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            //OnlineUserInfo oui = OnlineUsers.GetOnlineUser(olid);
            if (oluserinfo == null)
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                time = DateTime.Parse(oluserinfo.Ol_lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string authToken = Common.DES.Encode(string.Format("{0},{1},{2}", olid.ToString(), time, expires), this.Secret.Substring(0, 10)).Replace("+", "[");
            token.Token = authToken;
            if (Format == FormatType.JSON)
                returnStr = authToken;
            else
                returnStr = SerializationHelper.Serialize(token);
            return returnStr;
        }

        /// <summary>
        /// 获得会话
        /// </summary>
        /// <returns></returns>
        public string GetSession()
        {
            string returnStr = "";
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return returnStr;
            }

            if (GetParam("auth_token") == null)
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr;
            }

            string auth_token = GetParam("auth_token").ToString().Replace("[", "+");
            string a = SAS.Common.DES.Decode(auth_token, Secret.Substring(0, 10));

            string[] userstr = a.Split(',');
            if (userstr.Length != 3)
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr;
            }

            int olid = Utils.StrToInt(userstr[0], -1);
            OnlineUserInfo oluser = OnlineUsers.GetOnlineUser(olid);
            if (oluser == null)
            {
                ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                return returnStr;
            }
            string time = DateTime.Parse(oluser.Ol_lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            if (time != userstr[1])
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr;
            }
            byte[] md5_result = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(olid.ToString() + Secret));

            StringBuilder sessionkey_builder = new StringBuilder();

            foreach (byte b in md5_result)
                sessionkey_builder.Append(b.ToString("x2"));

            string sessionkey = string.Format("{0}-{1}", sessionkey_builder.ToString(), oluser.Userid.ToString());
            SessionInfo session = new SessionInfo();
            session.SessionKey = sessionkey;
            session.UId = oluser.Ol_ps_id;
            session.UserName = oluser.Ol_name;
            session.Expires = Utils.StrToInt(userstr[2], 0);
            //session.Expires = (DateTime.Parse(oluser.Lastupdatetime).AddMinutes(0 - GeneralConfigs.GetConfig().Onlinetimeout)).Ticks - DateTime.Now.Ticks;

            if (Format == FormatType.JSON)
                returnStr = string.Format(@"{{""session_key"":""{0}"",""uid"":{1},""user_name"":""{2}"",""expires"":{3}}}", sessionkey, Uid, session.UserName, session.Expires);
            else
                returnStr = SerializationHelper.Serialize(session);

            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0, GeneralConfigs.GetConfig().Onlinetimeout);

            return returnStr;
        }
    }
}
