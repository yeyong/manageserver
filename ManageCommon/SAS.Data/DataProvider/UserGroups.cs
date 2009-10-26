using System;
using System.Data;
using System.Text;

using SAS.Entity;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    public class UserGroups
    {
        /// <summary>
        /// 获得用户组数据
        /// </summary>
        /// <returns>用户组数据</returns>
        public static List<UserGroupInfo> GetUserGroupList()
        {
            DataTable dt = GetUserGroupForDataTable();
            List<UserGroupInfo> userGruopInfoList = new List<UserGroupInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                UserGroupInfo info = new UserGroupInfo();

                info.ug_id = TypeConverter.StrToInt(dr["ug_id"].ToString());
                info.ug_name = dr["ug_name"].ToString();
                info.ug_scorehight = TypeConverter.StrToInt(dr["ug_scorehight"].ToString());
                info.ug_scorelow = TypeConverter.StrToInt(dr["ug_scorelow"].ToString());
                info.ug_logo = dr["ug_logo"].ToString().Trim();
                info.ug_readaccess = TypeConverter.StrToInt(dr["ug_readaccess"].ToString().Trim());
                info.Ug_allowcusbbcode = TypeConverter.StrToInt(dr["ug_allowcusbbcode"].ToString().Trim());
                info.ug_allowvisit = TypeConverter.StrToInt(dr["ug_allowvisit"].ToString());
                info.ug_allowcommunity = TypeConverter.StrToInt(dr["ug_allowcommunity"].ToString());
                info.ug_allowdown = TypeConverter.StrToInt(dr["ug_allowdown"].ToString());
                info.ug_allowup = TypeConverter.StrToInt(dr["ug_allowup"].ToString());
                info.ug_allowsearch = TypeConverter.StrToInt(dr["ug_allowsearch"].ToString());
                info.ug_allowavatar = TypeConverter.StrToInt(dr["ug_allowavatar"].ToString());
                info.ug_allowshop = TypeConverter.StrToInt(dr["ug_allowshop"].ToString());
                info.ug_allowinvisible = TypeConverter.StrToInt(dr["ug_allowinvisible"].ToString());
                info.Ug_allowhidecode = TypeConverter.StrToInt(dr["ug_allowhidecode"].ToString());
                info.Ug_maxattachsize = TypeConverter.StrToInt(dr["ug_maxattachsize"].ToString());
                info.Ug_maxsizeperday = TypeConverter.StrToInt(dr["ug_maxsizeperday"].ToString());
                info.ug_attachextensions = dr["ug_attachextensions"].ToString();
                info.ug_maxspaceattachsize = TypeConverter.StrToInt(dr["ug_maxspaceattachsize"].ToString());
                info.ug_maxspacephotosize = TypeConverter.StrToInt(dr["ug_maxspacephotosize"].ToString());
                info.ug_maxsigsize = TypeConverter.StrToInt(dr["ug_maxsigsize"].ToString());
                info.ug_pg_id = TypeConverter.StrToInt(dr["ug_pg_id"].ToString());
                info.ug_color = dr["ug_color"].ToString();
                info.ug_isSystem = TypeConverter.StrToInt(dr["ug_isSystem"].ToString());
                info.Allowsetreadperm = TypeConverter.StrToInt(dr["allowsetreadperm"].ToString());
                info.Allowpostattach = TypeConverter.StrToInt(dr["allowpostattach"].ToString());
                info.Allowsetattachperm = TypeConverter.StrToInt(dr["allowsetattachperm"].ToString());
                info.Stars = TypeConverter.StrToInt(dr["stars"].ToString());
                info.Allowpost = TypeConverter.StrToInt(dr["allowpost"].ToString());
                info.Allowreply = TypeConverter.StrToInt(dr["allowreply"].ToString());
                info.Allowpostpoll = TypeConverter.StrToInt(dr["allowpostpoll"].ToString());
                info.Allowvote = TypeConverter.StrToInt(dr["allowvote"].ToString());
                info.Allownickname = TypeConverter.StrToInt(dr["allownickname"].ToString());
                info.Allowviewpro = TypeConverter.StrToInt(dr["allowviewpro"].ToString());
                info.Allowviewstats = TypeConverter.StrToInt(dr["allowviewstats"].ToString());
                info.Disableperiodctrl = TypeConverter.StrToInt(dr["disableperiodctrl"].ToString());
                info.Reasonpm = TypeConverter.StrToInt(dr["reasonpm"].ToString());
                info.Maxpmnum = TypeConverter.StrToInt(dr["maxpmnum"].ToString());
                
                userGruopInfoList.Add(info);
            }
            return userGruopInfoList;
        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserGroupForDataTable()
        {
            return DatabaseProvider.GetInstance().GetUserGroups();
        }

        /// <summary>
        /// 获取积分用户组
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCreditUserGroup()
        {
            return DatabaseProvider.GetInstance().GetUserGroup();
        }

        /// <summary>
        /// 更新用户组信息
        /// </summary>
        /// <param name="info">用户组信息</param>
        public static void UpdateUserGroup(UserGroupInfo info)
        {
            DatabaseProvider.GetInstance().UpdateUserGroup(info);
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="Creditshigher">积分上限</param>
        /// <param name="Creditslower">积分下限</param>
        /// <returns></returns>
        public static DataTable GetUserGroupByCreditsHigherAndLower(int Creditshigher, int Creditslower)
        {
            return DatabaseProvider.GetInstance().GetUserGroupByCreditsHigherAndLower(Creditshigher, Creditslower);
        }

        /// <summary>
        /// 更新在线表
        /// </summary>
        /// <param name="groupid">用户组ID</param>
        /// <param name="displayOrder">序号</param>
        /// <param name="img">图片</param>
        /// <param name="title">名称</param>
        /// <returns></returns>
        public static int UpdateOnlineList(int groupid, int displayorder, string img, string title)
        {
            return DatabaseProvider.GetInstance().UpdateOnlineList(groupid, displayorder, img, title);
        }

        /// <summary>
        /// 更新在线表
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateOnlineList(UserGroupInfo info)
        {
            DatabaseProvider.GetInstance().UpdateOnlineList(info);
        }

        /// <summary>
        /// 获取最小的积分上限
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMinCreditHigher()
        {
            return DatabaseProvider.GetInstance().GetMinCreditHigher();
        }

        /// <summary>
        /// 获取最大积分下限
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaxCreditLower()
        {
            return DatabaseProvider.GetInstance().GetMaxCreditLower();
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="Creditshigher">积分上限</param>
        /// <returns></returns>
        public static DataTable GetUserGroupByCreditshigher(int Creditshigher)
        {
            return DatabaseProvider.GetInstance().GetUserGroupByCreditshigher(Creditshigher);
        }

        /// <summary>
        /// 更新用户组积分上下限
        /// </summary>
        /// <param name="currentCreditsHigher"></param>
        /// <param name="Creditshigher"></param>
        public static void UpdateUserGroupCreidtsLower(int currentCreditsHigher, int Creditshigher)
        {
            DatabaseProvider.GetInstance().UpdateUserGroupCreidtsLower(currentCreditsHigher, Creditshigher);
        }

        /// <summary>
        /// 获取用户组数
        /// </summary>
        /// <param name="Creditshigher">积分上限</param>
        /// <returns></returns>
        public static int GetGroupCountByCreditsLower(int Creditshigher)
        {
            return DatabaseProvider.GetInstance().GetGroupCountByCreditsLower(Creditshigher);
        }

        public static void UpdateUserGroupsCreditsLowerByCreditsLower(int Creditslower, int Creditshigher)
        {
            DatabaseProvider.GetInstance().UpdateUserGroupsCreditsLowerByCreditsLower(Creditslower, Creditshigher);
        }


        public static void UpdateUserGroupsCreditsHigherByCreditsHigher(int Creditshigher, int Creditslower)
        {
            DatabaseProvider.GetInstance().UpdateUserGroupsCreditsHigherByCreditsHigher(Creditshigher, Creditslower);
        }
    }
}
