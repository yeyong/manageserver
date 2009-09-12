using System;
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
            DataRow[] dr = dt.Select("ol_ps_id<>'00000000-0000-0000-0000-000000000000'");
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
                        img = "<img src=\"" + BaseConfigs.GetSitePath + "images/groupicons/" + dr["img"].ToString() + "\" />";
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

        #endregion
    }
}
