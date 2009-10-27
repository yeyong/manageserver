﻿using System;
using System.Web;
using System.Data;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 在线用户操作类
    /// </summary>
    public class OnlineUsers
    {
        private static object SynObject = new object();

        /// <summary>
        /// 获得在线用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineAllUserCount()
        {
            int onlineUserCountCacheMinute = GeneralConfigs.GetConfig().OnlineUserCountCacheMinute;
            if (onlineUserCountCacheMinute == 0)
                return SAS.Data.DataProvider.OnlineUsers.GetOnlineAllUserCount();

            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            int onlineAllUserCount = TypeConverter.ObjectToInt(cache.RetrieveObject("/SAS/OnlineUserCount"));
            if (onlineAllUserCount != 0)
                return onlineAllUserCount;

            onlineAllUserCount = SAS.Data.DataProvider.OnlineUsers.GetOnlineAllUserCount();
            SAS.Cache.ICacheStrategy ics = new RssCacheStrategy();
            ics.TimeOut = onlineUserCountCacheMinute;
            cache.LoadCacheStrategy(ics);
            cache.AddObject("/SAS/OnlineUserCount", onlineAllUserCount);
            cache.LoadDefaultCacheStrategy();
            return onlineAllUserCount;
        }

        /// <summary>
        /// 返回缓存中在线用户总数
        /// </summary>
        /// <returns>缓存中在线用户总数</returns>
        public static int GetCacheOnlineAllUserCount()
        {
            int count = TypeConverter.StrToInt(Utils.GetCookie("onlineusercount"), 0);
            if (count == 0)
            {
                count = OnlineUsers.GetOnlineAllUserCount();
                Utils.WriteCookie("onlineusercount", count.ToString(), 3);
            }
            return count;
        }

        /// <summary>
        /// 清理之前的在线表记录(本方法在应用程序初始化时被调用)
        /// </summary>
        /// <returns></returns>
        public static int InitOnlineList()
        {
            return SAS.Data.DataProvider.OnlineUsers.CreateOnlineTable();
        }

        /// <summary>
        /// 复位在线表, 如果系统未重启, 仅是应用程序重新启动, 则不会重新创建
        /// </summary>
        /// <returns></returns>
        public static int ResetOnlineList()
        {
            try
            {
                // 取得在线表最后一条记录的tickcount字段 (因为本功能不要求特别精确)
                //int tickcount = DatabaseProvider.GetInstance().GetLastTickCount();
                // 如果距离现在系统运行时间小于10分钟
                if (System.Environment.TickCount < 600000 && System.Environment.TickCount > 0)
                    return SAS.Data.DataProvider.OnlineUsers.CreateOnlineTable();

                return -1;
            }
            catch
            {
                try
                {
                    return SAS.Data.DataProvider.OnlineUsers.CreateOnlineTable();
                }
                catch
                {
                    return -1;
                }
            }

        }

        /// <summary>
        /// 获得在线注册用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineUserCount()
        {
            return SAS.Data.DataProvider.OnlineUsers.GetOnlineUserCount();
        }

        #region 根据不同条件查询在线用户信息

        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns>线用户列表</returns>
        public static DataTable GetOnlineUserList(int totaluser, out int guest, out int user, out int invisibleuser)
        {
            DataTable dt = SAS.Data.DataProvider.OnlineUsers.GetOnlineUserListTable();
            int highestonlineusercount = TypeConverter.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1);

            if (totaluser > highestonlineusercount)
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser.ToString()) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }
            }
            // 统计用户
            DataRow[] dr = dt.Select("ol_ps_id>0");
            user = dr == null ? 0 : dr.Length;

            //统计隐身用户
            dr = dt.Select("invisible=1");
            invisibleuser = dr == null ? 0 : dr.Length;

            //统计游客
            guest = totaluser > user ? totaluser - user : 0;

            //返回当前版块的在线用户表
            return dt;
        }

        #endregion

        /// <summary>
        /// 返回在线用户图例
        /// </summary>
        /// <returns>在线用户图例</returns>
        private static DataTable GetOnlineGroupIconTable()
        {
            lock (SynObject)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                DataTable dt = cache.RetrieveObject("/SAS/OnlineIconTable") as DataTable;

                if (dt == null)
                {
                    dt = SAS.Data.DataProvider.OnlineUsers.GetOnlineGroupIconTable();
                    cache.AddObject("/SAS/OnlineIconTable", dt);
                }
                return dt;
            }
        }

        /// <summary>
        /// 返回用户组图标
        /// </summary>
        /// <param name="groupid">用户组</param>
        /// <returns>用户组图标</returns>
        public static string GetGroupImg(int groupid)
        {
            string img = "";
            DataTable dt = GetOnlineGroupIconTable();
            // 如果没有要显示的图例类型则返回""
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // 图例类型初始为:普通用户
                    // 如果有匹配的则更新为匹配的图例
                    if ((int.Parse(dr["ui_id"].ToString()) == 0 && img == "") || (int.Parse(dr["ui_id"].ToString()) == groupid))
                    {
                        img = "<img src=\"" + BaseConfigs.GetSitePath + "images/groupicons/" + dr["ui_img"].ToString() + "\" />";
                    }
                }
            }
            return img;
        }

        #region 查看指定的某一用户的详细信息

        public static OnlineUserInfo GetOnlineUser(int olid)
        {
            return SAS.Data.DataProvider.OnlineUsers.GetOnlineUser(olid);
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userid">在线用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户的详细信息</returns>
        private static OnlineUserInfo GetOnlineUser(int userid, string password)
        {
            return SAS.Data.DataProvider.OnlineUsers.GetOnlineUser(userid, password);
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <returns>用户的详细信息</returns>
        private static OnlineUserInfo GetOnlineUserByIP(int userid, string ip)
        {
            return SAS.Data.DataProvider.OnlineUsers.GetOnlineUserByIP(userid, ip);
        }

        /// <summary>
        /// 检查在线用户验证码是否有效
        /// </summary>
        /// <param name="olid">在组用户ID</param>
        /// <param name="verifycode">验证码</param>
        /// <returns>在组用户ID</returns>
        public static bool CheckUserVerifyCode(int olid, string verifycode)
        {
            return SAS.Data.DataProvider.OnlineUsers.CheckUserVerifyCode(olid, verifycode, LogicUtils.CreateAuthStr(5, false));
        }

        #endregion

        #region 添加新的在线用户


        /// <summary>
        /// Cookie中没有用户ID或则存的的用户ID无效时在在线表中增加一个游客.
        /// </summary>
        public static OnlineUserInfo CreateGuestUser(int timeout)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();

            onlineuserinfo.ol_ps_id = -1;
            onlineuserinfo.ol_name = "游客";
            onlineuserinfo.ol_nickName = "游客";
            onlineuserinfo.ol_password = "";
            onlineuserinfo.ol_ug_id = 7;
            onlineuserinfo.ol_img = GetGroupImg(7);
            onlineuserinfo.ol_pg_id = 0;
            onlineuserinfo.ol_invisible = 0;
            onlineuserinfo.ol_ip = SASRequest.GetIP();
            onlineuserinfo.ol_lastpostpmtime = "1900-1-1 00:00:00";
            onlineuserinfo.ol_lastsearchtime = "1900-1-1 00:00:00";
            onlineuserinfo.ol_lastupdatetime = "1900-1-1 00:00:00";
            onlineuserinfo.ol_action = 0;
            onlineuserinfo.ol_lastactivity = 0;
            onlineuserinfo.ol_verifycode = LogicUtils.CreateAuthStr(5, false);
            onlineuserinfo.ol_id = SAS.Data.DataProvider.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);

            return onlineuserinfo;
        }


        /// <summary>
        /// 增加一个会员信息到在线列表中。用户login.aspx或在线用户信息超时,但用户仍在线的情况下重新生成用户在线列表
        /// </summary>
        /// <param name="uid"></param>
        private static OnlineUserInfo CreateUser(int uid, int timeout)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();
            if (uid > 0)
            {
                ShortUserInfo ui = Users.GetShortUserInfo(uid);
                if (ui != null)
                {
                    onlineuserinfo.ol_ps_id = uid;
                    onlineuserinfo.ol_name = ui.Ps_name.Trim();
                    onlineuserinfo.ol_nickName = ui.Ps_nickName.Trim();
                    onlineuserinfo.ol_password = ui.Ps_password.Trim();
                    onlineuserinfo.ol_ug_id = short.Parse(ui.Ps_ug_id.ToString());
                    onlineuserinfo.ol_img = GetGroupImg(short.Parse(ui.Ps_ug_id.ToString()));
                    onlineuserinfo.ol_pg_id = short.Parse(ui.Ps_pg_id.ToString());
                    onlineuserinfo.ol_invisible = short.Parse(ui.ps_invisible.ToString());
                    onlineuserinfo.ol_ip = SASRequest.GetIP();
                    onlineuserinfo.ol_lastpostpmtime = "1900-1-1 00:00:00";
                    onlineuserinfo.ol_lastsearchtime = "1900-1-1 00:00:00";
                    onlineuserinfo.ol_lastupdatetime = "1900-1-1 00:00:00";
                    onlineuserinfo.ol_action = 0;
                    onlineuserinfo.ol_lastactivity = 0;
                    onlineuserinfo.ol_verifycode = LogicUtils.CreateAuthStr(5, false);
                    onlineuserinfo.ol_newpms = short.Parse(PrivateMessages.GetPrivateMessageCount(uid, 0, 1).ToString());
                    onlineuserinfo.ol_newnotices = short.Parse(Notices.GetNewNoticeCountByUid(uid).ToString());
                    onlineuserinfo.ol_id = SAS.Data.DataProvider.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);


                    //给管理人员发送关注通知
                    if (ui.Ps_pg_id > 0 && ui.Ps_pg_id < 4)
                    {
                        if (SAS.Data.DataProvider.Notices.ReNewNotice((int)Noticetype.AttentionNotice, ui.Ps_id) == 0)
                        {
                            NoticeInfo ni = new NoticeInfo();
                            ni.New = 1;
                            ni.Note = "请及时查看<a href=\"modcp.aspx?operation=attention&forumid=0\">需要关注的主题</a>";
                            ni.Postdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            ni.Type = Noticetype.AttentionNotice;
                            ni.Poster = "";
                            ni.Posterid = 0;
                            ni.Uid = ui.Ps_id;
                            Notices.CreateNoticeInfo(ni);
                        }
                    }
                    SAS.Data.DataProvider.OnlineUsers.SetUserOnlineState(uid, 1);

                    HttpCookie cookie = HttpContext.Current.Request.Cookies["sas"];
                    if (cookie != null)
                    {
                        //cookie.Values["tpp"] = ui.Tpp.ToString();
                        //cookie.Values["ppp"] = ui.Ppp.ToString();
                        if (HttpContext.Current.Request.Cookies["sas"]["expires"] != null)
                        {
                            int expires = TypeConverter.StrToInt(HttpContext.Current.Request.Cookies["sas"]["expires"].ToString(), 0);
                            if (expires > 0)
                            {
                                cookie.Expires = DateTime.Now.AddMinutes(TypeConverter.StrToInt(HttpContext.Current.Request.Cookies["sas"]["expires"].ToString(), 0));
                            }
                        }
                    }

                    string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
                    if (!Utils.StrIsNullOrEmpty(cookieDomain) && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && LogicUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
                        cookie.Domain = cookieDomain;
                    HttpContext.Current.Response.AppendCookie(cookie);
                }
                else
                {
                    onlineuserinfo = CreateGuestUser(timeout);
                }
            }
            else
            {
                onlineuserinfo = CreateGuestUser(timeout);
            }

            return onlineuserinfo;
        }


        /// <summary>
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// </summary>
        /// <param name="passwordkey">论坛passwordkey</param>
        /// <param name="timeout">在线超时时间</param>
        /// <param name="passwd">用户密码</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout, int uid, string passwd)
        {
            lock (SynObject)
            {
                OnlineUserInfo onlineuser = new OnlineUserInfo();
                string ip = SASRequest.GetIP();
                int userid = TypeConverter.StrToInt(LogicUtils.GetCookie("userid"), uid);
                string password = (Utils.StrIsNullOrEmpty(passwd) ? LogicUtils.GetCookiePassword(passwordkey) : LogicUtils.GetCookiePassword(passwd, passwordkey));

                // 如果密码非Base64编码字符串则怀疑被非法篡改, 直接置身份为游客
                if (password.Length == 0 || !Utils.IsBase64String(password))
                    userid = -1;

                if (userid != -1)
                {
                    onlineuser = GetOnlineUser(userid, password);

                    //更新流量统计
                    if (!SASRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                        Stats.UpdateStatCount(false, onlineuser != null);

                    if (onlineuser != null)
                    {
                        if (onlineuser.ol_ip != ip)
                        {
                            UpdateIP(onlineuser.ol_id, ip);
                            onlineuser.ol_ip = ip;
                            return onlineuser;
                        }
                    }
                    else
                    {
                        // 判断密码是否正确
                        userid = Users.CheckPassword(userid, password, false);
                        if (userid != -1)
                        {
                            SAS.Data.DataProvider.OnlineUsers.DeleteRowsByIP(ip);
                            CheckIp(ip);
                            return CreateUser(userid, timeout);
                        }
                        else
                        {
                            CheckIp(ip);
                            // 如密码错误则在在线表中创建游客
                            onlineuser = GetOnlineUserByIP(-1, ip);
                            if (onlineuser == null)
                                return CreateGuestUser(timeout);
                        }
                    }
                }
                else
                {
                    onlineuser = GetOnlineUserByIP(-1, ip);
                    //更新流量统计
                    if (!SASRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                        Stats.UpdateStatCount(true, onlineuser != null);

                    if (onlineuser == null)
                        return CreateGuestUser(timeout);
                }

                onlineuser.ol_lastupdatetime = Utils.GetDateTime();
                return onlineuser;
            }
        }

        /// <summary>
        /// 检查ip地址是否合法
        /// </summary>
        /// <param name="ip"></param>
        private static void CheckIp(string ip)
        {
            string errmsg = "";
            //判断IP地址是否合法,需要重构
            SAS.Common.Generic.List<IpInfo> list = Caches.GetBannedIpList();

            foreach (IpInfo ipinfo in list)
            {
                if (ip == (string.Format("{0}.{1}.{2}.{3}", ipinfo.Ip1, ipinfo.Ip2, ipinfo.Ip3, ipinfo.Ip4)))
                {
                    errmsg = "您的ip被封,于" + ipinfo.Expiration + "后解禁";
                    break;
                }

                if (ipinfo.Ip4.ToString() == "*")
                {
                    if ((TypeConverter.StrToInt(ip.Split('.')[0], -1) == ipinfo.Ip1) && (TypeConverter.StrToInt(ip.Split('.')[1], -1) == ipinfo.Ip2) && (TypeConverter.StrToInt(ip.Split('.')[2], -1) == ipinfo.Ip3))
                    {
                        errmsg = "您所在的ip段被封,于" + ipinfo.Expiration + "后解禁";
                        break;
                    }
                }
            }

            if (errmsg != string.Empty)
                HttpContext.Current.Response.Redirect(BaseConfigs.GetSitePath + "tools/error.htm?templatepath=default&msg=" + Utils.UrlEncode(errmsg));
        }

        /// <summary>
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// </summary>
        /// <param name="passwordkey">用户密码</param>
        /// <param name="timeout">在线超时时间</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout)
        {
            return UpdateInfo(passwordkey, timeout, -1, "");
        }

        #endregion

        #region 在组用户信息更新

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        /// <param name="timeout">过期时间</param>
        public static void UpdateAction(int olid, int action, int inid, int timeout)
        {
            // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
            if ((timeout < 0) && (Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), Environment.TickCount) < 300000))
                Utils.WriteCookie("lastolupdate", Environment.TickCount.ToString());
            else
                UpdateAction(olid, action, inid);
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        public static void UpdateAction(int olid, int action, int inid)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
            {
                SAS.Data.DataProvider.OnlineUsers.UpdateAction(olid, action, inid);
            }
        }


        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作id</param>
        /// <param name="fid">版块id</param>
        /// <param name="forumname">版块名</param>
        /// <param name="tid">主题id</param>
        /// <param name="topictitle">主题名</param>
        /// <param name="timeout">超时时间</param>
        public static void UpdateAction(int olid, int action, int fid, string forumname, int tid, string topictitle, int timeout)
        {
            // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
            if ((timeout < 0) && (System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) < 300000))
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
            else
            {
                if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
                {
                    if ((action == UserAction.ShowForum.ActionID) || (action == UserAction.PostTopic.ActionID) || (action == UserAction.ShowTopic.ActionID) || (action == UserAction.PostReply.ActionID))
                        forumname = forumname.Length > 40 ? forumname.Substring(0, 37) + "..." : forumname;

                    if ((action == UserAction.ShowTopic.ActionID) || (action == UserAction.PostReply.ActionID))
                        topictitle = topictitle.Length > 40 ? topictitle.Substring(0, 37) + "..." : topictitle;

                    SAS.Data.DataProvider.OnlineUsers.UpdateAction(olid, action, fid, forumname, tid, topictitle);
                }
            }
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="timeout">超时时间</param>
        private static void UpdateLastTime(int olid, int timeout)
        {
            // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
            if ((timeout < 0) && (System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) < 300000))
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
            else
                SAS.Data.DataProvider.OnlineUsers.UpdateLastTime(olid);
        }

        /// <summary>
        /// 更新用户最后发帖时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public static void UpdatePostTime(int olid)
        {
            SAS.Data.DataProvider.OnlineUsers.UpdatePostTime(olid);
        }

        /// <summary>
        /// 更新用户最后发短消息时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public static void UpdatePostPMTime(int olid)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
            {
                SAS.Data.DataProvider.OnlineUsers.UpdatePostPMTime(olid);
            }
        }

        /// <summary>
        /// 更新在线表中指定用户是否隐身
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="invisible">是否隐身</param>
        public static void UpdateInvisible(int olid, int invisible)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
            {
                SAS.Data.DataProvider.OnlineUsers.UpdateInvisible(olid, invisible);
            }
        }

        /// <summary>
        /// 更新在线表中指定用户的用户密码
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="password">用户密码</param>
        public static void UpdatePassword(int olid, string password)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
            {
                SAS.Data.DataProvider.OnlineUsers.UpdatePassword(olid, password);
            }
        }


        /// <summary>
        /// 更新用户IP地址
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="ip">ip地址</param>
        public static void UpdateIP(int olid, string ip)
        {
            SAS.Data.DataProvider.OnlineUsers.UpdateIP(olid, ip);
        }

        /// <summary>
        /// 更新用户最后搜索时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public static void UpdateSearchTime(int olid)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization == 1)
            {
                SAS.Data.DataProvider.OnlineUsers.UpdateSearchTime(olid);
            }
        }

        /// <summary>
        /// 更新用户的用户组
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="groupid">组名</param>
        public static void UpdateGroupid(int userid, int groupid)
        {
            SAS.Data.DataProvider.OnlineUsers.UpdateGroupid(userid, groupid);
        }

        #endregion

        /// <summary>
        /// 删除在线表中指定在线id的行
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns></returns>
        public static int DeleteRows(int olid)
        {
            return SAS.Data.DataProvider.OnlineUsers.DeleteRows(olid);
        }

        #region 条件编译的方法

        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns></returns>
        public static SAS.Common.Generic.List<OnlineUserInfo> GetForumOnlineUserCollection(int forumid, out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            SAS.Common.Generic.List<OnlineUserInfo> coll = SAS.Data.DataProvider.OnlineUsers.GetForumOnlineUserCollection(forumid);

            //在线游客
            guest = 0;
            //在线隐身用户
            invisibleuser = 0;
            //当前版块在线总用户数
            totaluser = coll.Count;

            foreach (OnlineUserInfo onlineUserInfo in coll)
            {
                if (onlineUserInfo.ol_ps_id == -1)
                    guest++;

                if (onlineUserInfo.ol_invisible == 1)
                    invisibleuser++;
            }

            //统计用户
            user = totaluser - guest;
            //返回当前版块的在线用户表
            return coll;
        }


        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns></returns>
        public static SAS.Common.Generic.List<OnlineUserInfo> GetOnlineUserCollection(out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            SAS.Common.Generic.List<OnlineUserInfo> coll = SAS.Data.DataProvider.OnlineUsers.GetOnlineUserCollection();

            //在线注册用户数
            user = 0;
            //在线隐身用户数
            invisibleuser = 0;
            //在线总用户数
            totaluser = coll.Count;

            foreach (OnlineUserInfo onlineUserInfo in coll)
            {
                if (onlineUserInfo.ol_ps_id > 0)
                    user++;

                if (onlineUserInfo.ol_invisible == 1)
                    invisibleuser++;
            }

            if (totaluser > TypeConverter.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1))
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser.ToString()) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }
            }

            //统计游客
            guest = totaluser > user ? totaluser - user : 0;

            //返回当前版块的在线用户集合
            return coll;
        }

        /// <summary>
        /// 更新在线时间
        /// </summary>
        /// <param name="oltimespan">在线时间间隔</param>
        /// <param name="uid">当前用户id</param>
        public static void UpdateOnlineTime(int oltimespan, int uid)
        {
            //为0代表关闭统计功能
            if (oltimespan != 0)
            {
                if (Utils.StrIsNullOrEmpty(Utils.GetCookie("lastactivity", "onlinetime")))
                    Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());

                if ((System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastactivity", "onlinetime"), System.Environment.TickCount) >= oltimespan * 60 * 1000))
                {
                    SAS.Data.DataProvider.OnlineUsers.UpdateOnlineTime(oltimespan, uid);
                    Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());

                    //判断是否同步oltime (登录后的第一次onlinetime更新的时候或者在线超过1小时)
                    if (Utils.StrIsNullOrEmpty(Utils.GetCookie("lastactivity", "oltime")) || (System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastactivity", "onlinetime"), System.Environment.TickCount) >= 60 * 60 * 1000))
                    {
                        SAS.Data.DataProvider.OnlineUsers.SynchronizeOnlineTime(uid);
                        Utils.WriteCookie("lastactivity", "oltime", System.Environment.TickCount.ToString());
                    }
                }
            }
        }

        #endregion



        /// <summary>
        /// 根据Uid获得Olid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int GetOlidByUid(int uid)
        {
            return SAS.Data.DataProvider.OnlineUsers.GetOlidByUid(uid);
        }

        /// <summary>
        /// 删除在线表中Uid的用户
        /// </summary>
        /// <param name="uid">要删除用户的Uid</param>
        /// <returns></returns>
        public static int DeleteUserByUid(int uid)
        {
            return DeleteRows(GetOlidByUid(uid));
        }

        /// <summary>
        /// 更新用户新短消息数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="count">更新数</param>
        /// <returns></returns>
        public static int UpdateNewPms(int olid, int count)
        {
            return SAS.Data.DataProvider.OnlineUsers.UpdateNewPms(olid, count);
        }

        /// <summary>
        /// 更新用户新通知数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="pluscount">增加量</param>
        /// <returns></returns>
        public static int UpdateNewNotices(int olid, int pluscount)
        {
            return SAS.Data.DataProvider.OnlineUsers.UpdateNewNotices(olid, pluscount);
        }       
    }
}
