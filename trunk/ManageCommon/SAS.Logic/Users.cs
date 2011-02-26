using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Plugin.PasswordMode;

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
        /// 通过用户名获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string userName)
        {
            return Data.DataProvider.Users.GetUserInfo(userName);
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
        /// 第三方用户密码检查
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static UserInfo CheckThirdPartPassword(string userName, string passWord, int question, string answer)
        {
            UserInfo userInfo = Users.GetUserInfo(userName);

            //当安全问题未通过时
            if (userInfo != null && GeneralConfigs.GetConfig().Secques == 1 &&
                userInfo.Ps_secques.Trim() != LogicUtils.GetUserSecques(question, answer))
                return null;

            if (PasswordModeProvider.GetInstance() != null && PasswordModeProvider.GetInstance().CheckPassword(userInfo, passWord))
                return userInfo;

            return null;
        }

        /// <summary>
        /// 检测密码和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckPasswordAndSecques(string username, string password, bool originalpassword, int questionid, string answer)
        {
            return SAS.Data.DataProvider.Users.CheckPasswordAndSecques(username, password, originalpassword, LogicUtils.GetUserSecques(questionid, answer));
        }

        /// <summary>
        /// 根据指定的email查找用户并返回用户uid
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        public static bool ValidateEmail(string email)
        {
            if (GeneralConfigs.GetConfig().Doublee == 0 && SAS.Data.DataProvider.Users.FindUserEmail(email) != -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据指定的email查找用户并返回用户uid
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        public static bool ValidateEmail(string email, int uid)
        {
            int userid = SAS.Data.DataProvider.Users.FindUserEmail(email);
            if (GeneralConfigs.GetConfig().Doublee == 0 && userid != -1 && uid != userid)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 得到站中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public static int GetUserCount(string condition)
        {
            return (condition == "") ? SAS.Data.DataProvider.Users.GetUserCount() : Data.DataProvider.Users.GetUserCount(condition);
        }

        /// <summary>
        /// 创建新用户.
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>返回用户ID, 如果已存在该用户名则返回-1</returns>
        public static int CreateUser(UserInfo userinfo)
        {
            if (GetUserId(userinfo.Ps_name) > 0)
                return -1;

            return SAS.Data.DataProvider.Users.CreateUser(userinfo);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateUser(UserInfo userinfo)
        {
            if (userinfo == null)
                return false;

            return SAS.Data.DataProvider.Users.UpdateUser(userinfo);
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
        /// 更新Email验证信息
        /// </summary>
        /// <param name="authstr">验证字符串</param>
        /// <param name="authtime">验证时间</param>
        /// <param name="uid">用户Id</param>
        public static void UpdateEmailValidateInfo(string authstr, DateTime authTime, int uid)
        {
            SAS.Data.DataProvider.Users.UpdateEmailValidateInfo(authstr, authTime, uid);
        }

        /// <summary>
        /// 更新指定用户的个人资料
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>如果用户不存在则为false, 否则为true</returns>
        public static bool UpdateUserProfile(UserInfo userinfo)
        {
            if (SAS.Data.DataProvider.Users.GetShortUserInfo(userinfo.Ps_id) == null)
                return false;

            SAS.Data.DataProvider.Users.UpdateUserProfile(userinfo);
            return true;
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

        /// <summary>
        /// 更改用户组用户的管理权限
        /// </summary>
        /// <param name="adminId">管理组Id</param>
        /// <param name="groupId">用户组Id</param>
        public static void UpdateUserAdminIdByGroupId(int adminId, int groupId)
        {
            SAS.Data.DataProvider.Users.UpdateUserAdminIdByGroupId(adminId, groupId);
        }

        /// <summary>
        /// 获取团队成员信息
        /// </summary>
        /// <param name="usernames"></param>
        public static DataTable GetMemberListByUserName(string usernames)
        {
            return SAS.Data.DataProvider.Users.GetMemberList(usernames);
        }
    }
}
