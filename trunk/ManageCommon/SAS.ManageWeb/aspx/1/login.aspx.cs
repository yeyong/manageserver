using System;
using System.Data;
using System.Text;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class login : BasePage
    {
        #region 页面变量
        /// <summary>
        /// 登录所使用的用户名
        /// </summary>
        public string postusername = Utils.UrlDecode(SASRequest.GetString("postusername")).Trim();
        /// <summary>
        /// 登陆时的密码验证信息
        /// </summary>
        public string loginauth = SASRequest.GetString("loginauth");
        /// <summary>
        /// 登陆时提交的密码
        /// </summary>
        public string postpassword = "";
        /// <summary>
        /// 登陆成功后跳转的链接
        /// </summary>
        public string referer = SASRequest.GetString("referer");
        /// <summary>
        /// 是否跨页面提交
        /// </summary>
        public bool loginsubmit = SASRequest.GetString("loginsubmit") == "true" ? true : false;

        //public int inapi = 0;

        #endregion               

        protected override void ShowPage()
        {
            pagetitle = "用户登录";

            if (userid != -1)
            {
                SetUrl(BaseConfigs.GetSitePath);
                AddMsgLine("您已经登录，无须重复登录");
                ispost = true;
                //SetLeftMenuRefresh();

                //if (APIConfigs.GetConfig().Enable)
                //    APILogin(APIConfigs.GetConfig());
            }

            if (LoginLogs.UpdateLoginLog(SASRequest.GetIP(), false) >= 5)
            {
                AddErrLine("您已经多次输入密码错误, 请15分钟后再登录");
                loginsubmit = false;
                return;
            }

            SetReUrl();

            //如果提交...
            if (SASRequest.IsPost())
            {
                SetBackLink();

                //如果没输入验证码就要求用户填写
                if (isseccode && SASRequest.GetString("vcode") == "")
                {
                    postusername = SASRequest.GetString("username");
                    loginauth = DES.Encode(SASRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    loginsubmit = true;
                    return;
                }

                if ((Users.GetUserId(SASRequest.GetString("username")) == 0))
                    AddErrLine("用户不存在");

                if (Utils.StrIsNullOrEmpty(SASRequest.GetString("password")) && Utils.StrIsNullOrEmpty(SASRequest.GetString("loginauth")))
                    AddErrLine("密码不能为空");

                if (IsErr()) return;

                ShortUserInfo userInfo = GetShortUserInfo();

                if (userInfo != null)
                {
                    #region 当前用户所在用户组为"禁止访问"或"等待激活"时

                    //if (userInfo.Ps_ug_id == 4 || userInfo.Ps_ug_id == 5) 
                    //{
                    //    //先改为第一个积分组
                    //    userInfo.Ps_ug_id = 11;
                    //    Users.UpdateUserGroup(userInfo.Ps_id, 11);
                    //}

                    if (userInfo.Ps_ug_id == 5)// 5-禁止访问
                    {
                        AddErrLine("您所在的用户组，已经被禁止访问");
                        return;
                    }

                    if (userInfo.Ps_ug_id == 8)
                    {
                        if (config.Regverify == 1)
                            AddMsgLine("请您到您的邮箱中点击激活链接来激活您的帐号");
                        else if (config.Regverify == 2)
                            AddMsgLine("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
                        else
                            AddErrLine("抱歉, 您的用户身份尚未得到验证");

                        loginsubmit = false;
                        return;
                    }
                    #endregion

                    if (!Utils.StrIsNullOrEmpty(userInfo.Ps_secques) && loginsubmit && Utils.StrIsNullOrEmpty(SASRequest.GetString("loginauth")))
                    {
                        loginauth = DES.Encode(SASRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    }
                    else
                    {
                        //通过api整合的程序登录
                        //if (APIConfigs.GetConfig().Enable)
                        //    APILogin(APIConfigs.GetConfig());


                        AddMsgLine("登录成功, 返回登录前页面");

                        #region 无延迟更新在线信息和相关用户信息
                        oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                        olid = oluserinfo.Ol_id;
                        username = SASRequest.GetString("username");
                        userid = userInfo.Ps_id;
                        usergroupinfo = UserGroups.GetUserGroupInfo(userInfo.Ps_ug_id);
                        useradminid = usergroupinfo.ug_pg_id; // 根据用户组得到相关联的管理组id

                        LogicUtils.WriteUserCookie(userInfo.Ps_id, TypeConverter.StrToInt(SASRequest.GetString("expires"), -1),
                                config.Passwordkey, SASRequest.GetInt("templateid", 0), SASRequest.GetInt("loginmode", -1));
                        OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
                        LoginLogs.DeleteLoginLog(SASRequest.GetIP());
                        Users.UpdateUserCreditsAndVisit(userInfo.Ps_id, SASRequest.GetIP());
                        #endregion

                        loginsubmit = false;
                        string reurl = Utils.UrlDecode(LogicUtils.GetReUrl());
                        SetUrl(reurl.IndexOf("register.aspx") < 0 ? reurl : forumpath + "index.aspx");

                        //SetLeftMenuRefresh();
                        MsgForward("login_succeed", true);
                    }
                }
                else
                {
                    int errcount = LoginLogs.UpdateLoginLog(SASRequest.GetIP(), true);
                    if (errcount > 5)
                        AddErrLine("您已经输入密码5次错误, 请15分钟后再试");
                    else
                        AddErrLine(string.Format("密码或安全提问第{0}次错误, 您最多有5次机会重试", errcount));
                }
            }
        }

        /// <summary>
        /// 设置BackLink
        /// </summary>
        private void SetBackLink()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in System.Web.HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (key != "postusername")
                    builder.AppendFormat("&{0}={1}", key, SASRequest.GetQueryString(key));
            }
            base.SetBackLink("login.aspx?postusername=" + Utils.UrlEncode(SASRequest.GetString("username")) + builder);
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <returns></returns>
        private ShortUserInfo GetShortUserInfo()
        {
            postpassword = !Utils.StrIsNullOrEmpty(loginauth) ?
                    DES.Decode(loginauth.Replace("[", "+"), config.Passwordkey) :
                    SASRequest.GetString("password");

            postusername = Utils.StrIsNullOrEmpty(postusername) ? SASRequest.GetString("username") : postusername;

            int uid = -1;
            switch (config.Passwordmode)
            {
                //case 1://动网兼容模式
                //    {
                //        if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
                //            uid = Users.CheckDvBbsPasswordAndSecques(postusername, postpassword, SASRequest.GetInt("question", 0), SASRequest.GetString("answer"));
                //        else
                //            uid = Users.CheckDvBbsPassword(postusername, postpassword);
                //        break;
                //    }
                case 0://默认模式
                    {
                        if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
                            uid = Users.CheckPasswordAndSecques(postusername, postpassword, true, SASRequest.GetInt("question", 0), SASRequest.GetString("answer"));
                        else
                            uid = Users.CheckPassword(postusername, postpassword, true);
                        break;
                    }
                default: //第三方加密验证模式
                    {
                        return (ShortUserInfo)Users.CheckThirdPartPassword(postusername, postpassword, SASRequest.GetInt("question", 0), SASRequest.GetString("answer"));
                    }
            }
            return uid > 0 ? Users.GetShortUserInfo(uid) : null;
        }

        /// <summary>
        /// 设置reurl
        /// </summary>
        private void SetReUrl()
        {
            //未提交或跨页提交时
            if (!SASRequest.IsPost() || referer != "")
            {
                string r = "";
                if (referer != "")
                    r = referer;
                else
                {
                    if ((SASRequest.GetUrlReferrer() == "") || (SASRequest.GetUrlReferrer().IndexOf("login") > -1) || SASRequest.GetUrlReferrer().IndexOf("logout") > -1)
                        r = "index.aspx";
                    else
                        r = SASRequest.GetUrlReferrer();
                }
                Utils.WriteCookie("reurl", (SASRequest.GetQueryString("reurl") == "" || SASRequest.GetQueryString("reurl").IndexOf("login.aspx") > -1) ? r : SASRequest.GetQueryString("reurl"));
            }
        }

        ///// <summary>
        ///// 测试用登录临时方法
        ///// </summary>
        //public void virtualLogin()
        //{

            

        //    LogicUtils.WriteUserCookie(
        //                        1,
        //                        TypeConverter.StrToInt(SASRequest.GetString("expires"), -1),
        //                        config.Passwordkey,
        //                        SASRequest.GetInt("templateid", 0),
        //                        SASRequest.GetInt("loginmode", -1));
        //    OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0, config.Onlinetimeout);
        //    LoginLogs.DeleteLoginLog(SASRequest.GetIP());
        //    Users.UpdateUserCreditsAndVisit(1, SASRequest.GetIP());
        //}
    }
}
