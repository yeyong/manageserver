using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 用户操作类
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 返回指定用户的完整信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static UserInfo GetUserInfo(int uid)
        {
            return SAS.Data.DataProvider.Users.GetUserInfo(uid);
        }

        /// <summary>
        /// 返回指定用户的简短信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static ShortUserInfo GetShortUserInfo(int uid)
        {
            return SAS.Data.DataProvider.Users.GetShortUserInfo(uid);
        }

        /// <summary>
        /// 根据IP查找用户
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>用户信息</returns>
        public static string CheckRegisterDateDiff(string ip)
        {
            ShortUserInfo userinfo = SAS.Data.DataProvider.Users.GetShortUserInfoByIP(ip);

            if (GeneralConfigs.GetConfig().Regctrl > 0 && userinfo != null)
            {
                int Interval = Utils.StrDateDiffHours(userinfo.Ps_createDate, GeneralConfigs.GetConfig().Regctrl);
                if (Interval <= 0)
                    return "抱歉, 系统设置了IP注册间隔限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册";
            }

            if (GeneralConfigs.GetConfig().Ipregctrl.Trim() != "" && Utils.InIPArray(SASRequest.GetIP(), Utils.SplitString(GeneralConfigs.GetConfig().Ipregctrl, "\n")) && userinfo != null)
            {
                int Interval = Utils.StrDateDiffHours(userinfo.Ps_createDate, 72);
                if (Interval < 0)
                    return "抱歉, 系统设置了特殊IP注册限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册";
            }
            return null;
        }

        /// <summary>
        /// 根据用户名返回用户id
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户id</returns>
        public static int GetUserId(string username)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.GetShortUserInfoByName(username);
            return (userInfo != null) ? userInfo.Ps_id : 0;
        }

        /// <summary>
        /// 获得用户列表DataTable
        /// </summary>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageindex">当前页数</param>
        /// <returns>用户列表DataTable</returns>
        public static DataTable GetUserList(int pagesize, int pageindex, string column, string ordertype)
        {
            DataTable dt = SAS.Data.DataProvider.Users.GetUserList(pagesize, pageindex, column, ordertype);
            dt.Columns.Add("grouptitle");
            dt.Columns.Add("olimg");
            foreach (DataRow dataRow in dt.Rows)
            {
                UserGroupInfo group = UserGroups.GetUserGroupInfo(Utils.StrToInt(dataRow["ps_ug_id"], 0));

                if (Utils.StrIsNullOrEmpty(group.ug_color))
                    dataRow["grouptitle"] = group.ug_name;
                else
                    dataRow["grouptitle"] = string.Format("<font color='{1}'>{0}</font>", group.ug_name, group.ug_color);

                dataRow["olimg"] = OnlineUsers.GetGroupImg(Utils.StrToInt(dataRow["ps_ug_id"], 0));
            }
            return dt;
        }

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckPassword(string username, string password, bool originalpassword)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.CheckPassword(username, password, originalpassword);

            return userInfo == null ? -1 : userInfo.Ps_id;
        }

        /// <summary>
        /// 判断指定用户密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static int CheckPassword(int uid, string password, bool originalpassword)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.CheckPassword(uid, password, originalpassword);

            return userInfo == null ? -1 : userInfo.Ps_id;
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public static int GetUserCount(string condition)
        {
            return (condition == "") ? SAS.Data.DataProvider.Users.GetUserCount() : Data.DataProvider.Users.GetUserCount(condition);
        }

        /// <summary>
        /// 更新用户组
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="groupID">用户组ID</param>
        public static void UpdateUserGroup(int uid, int groupId)
        {
            //Discuz.Data.Users.UpdateUserGroup(uid, groupID);
            UpdateUserGroup(uid.ToString(), groupId);
        }

        /// <summary>
        /// 更新用户的用户组信息
        /// </summary>
        /// <param name="uidList">用户ID列表</param>
        /// <param name="groupId">用户组ID</param>
        public static void UpdateUserGroup(string uidList, int groupId)
        {
            SAS.Data.DataProvider.Users.UpdateUserGroup(uidList, groupId);
        }

        /// <summary>
        /// 更新用户积分和最后登录时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public static void UpdateUserCreditsAndVisit(int uid, string ip)
        {
            //UserCredits.UpdateUserCredits(uid);
            SAS.Data.DataProvider.Users.UpdateUserLastvisit(uid, ip);
        }

        /// <summary>
        /// 更新用户到禁言组
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        public static void UpdateUserToStopTalkGroup(string uidList)
        {
            SAS.Data.DataProvider.Users.UpdateUserToStopTalkGroup(uidList);
        }

        /// <summary>
        /// 获取当前页用户列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentPage">当前页数</param>
        /// <returns></returns>
        public static DataTable GetUserListByCurrentPage(int pageSize, int currentPage)
        {
            return Data.DataProvider.Users.GetUserListByCurrentPage(pageSize, currentPage);
        }

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="isLike">模糊查询</param>
        /// <param name="isPostDateTime">发帖日期</param>
        /// <param name="userName">用户名</param>
        /// <param name="nickName">昵称</param>
        /// <param name="userGroup">用户组</param>
        /// <param name="email">Email</param>
        /// <param name="credits_Start">积分起始值</param>
        /// <param name="credits_End">积分结束值 </param>
        /// <param name="lastIp">最全登录IP</param>
        /// <param name="posts">帖数</param>
        /// <param name="digestPosts">精华帖数</param>
        /// <param name="uid">Uid</param>
        /// <param name="joindateStart">注册起始日期</param>
        /// <param name="joindateEnd">注册结束日期</param>
        /// <returns></returns>
        public static string GetUsersSearchCondition(bool isLike, bool isPostDateTime, string userName, string nickName,
            string userGroup, string email, string credits_Start, string credits_End, string lastIp, string posts, string digestPosts,
            string uid, string joindateStart, string joindateEnd)
        {
            return Data.DataProvider.Users.GetUsersSearchCondition(isLike, isPostDateTime, userName, nickName,
                userGroup, email, credits_Start, credits_End, lastIp, posts, digestPosts, uid, joindateStart, joindateEnd);
        }

        /// <summary>
        /// 获取按条件搜索得到的用户列表
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <returns></returns>
        public static DataTable GetUsersByCondition(string searchCondition)
        {
            return Data.DataProvider.Users.GetUsersByCondition(searchCondition);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="currentpage">当前页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetUserList(int pagesize, int currentpage, string condition)
        {
            return Data.DataProvider.Users.GetUserList(pagesize, currentpage, condition);
        }

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="getstring"></param>
        /// <returns></returns>
        public static string GetUserListCondition(string getstring)
        {
            return SAS.Data.DataProvider.Users.GetUserListCondition(getstring);
        }
    }
}
