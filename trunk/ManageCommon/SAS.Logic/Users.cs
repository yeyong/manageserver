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
        public static UserInfo GetUserInfo(Guid uid)
        {
            return SAS.Data.DataProvider.Users.GetUserInfo(uid);
        }

        /// <summary>
        /// 返回指定用户的简短信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static ShortUserInfo GetShortUserInfo(Guid uid)
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
        public static Guid GetUserId(string username)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.GetShortUserInfoByName(username);
            return (userInfo != null) ? userInfo.Ps_id : new Guid("00000000-0000-0000-0000-000000000000");
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
        public static Guid CheckPassword(string username, string password, bool originalpassword)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.CheckPassword(username, password, originalpassword);

            return userInfo == null ? new Guid("00000000-0000-0000-0000-000000000000") : userInfo.Ps_id;
        }

        /// <summary>
        /// 判断指定用户密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static Guid CheckPassword(Guid uid, string password, bool originalpassword)
        {
            ShortUserInfo userInfo = SAS.Data.DataProvider.Users.CheckPassword(uid, password, originalpassword);

            return userInfo == null ? new Guid("00000000-0000-0000-0000-000000000000") : userInfo.Ps_id;
        }
    }
}
