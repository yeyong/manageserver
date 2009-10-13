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
                info.ug_allowvisit = TypeConverter.StrToInt(dr["ug_allowvisit"].ToString());
                info.ug_allowcommunity = TypeConverter.StrToInt(dr["ug_allowcommunity"].ToString());
                info.ug_allowdown = TypeConverter.StrToInt(dr["ug_allowdown"].ToString());
                info.ug_allowup = TypeConverter.StrToInt(dr["ug_allowup"].ToString());
                info.ug_allowsearch = TypeConverter.StrToInt(dr["ug_allowsearch"].ToString());
                info.ug_allowavatar = TypeConverter.StrToInt(dr["ug_allowavatar"].ToString());
                info.ug_allowshop = TypeConverter.StrToInt(dr["ug_allowshop"].ToString());
                info.ug_allowinvisible = TypeConverter.StrToInt(dr["ug_allowinvisible"].ToString());
                info.ug_maxattachsize = TypeConverter.StrToInt(dr["ug_maxattachsize"].ToString());
                info.ug_maxsizeperday = TypeConverter.StrToInt(dr["ug_maxsizeperday"].ToString());
                info.ug_attachextensions = dr["ug_attachextensions"].ToString();
                info.ug_maxspaceattachsize = TypeConverter.StrToInt(dr["ug_maxspaceattachsize"].ToString());
                info.ug_maxspacephotosize = TypeConverter.StrToInt(dr["ug_maxspacephotosize"].ToString());
                info.ug_maxsigsize = TypeConverter.StrToInt(dr["ug_maxsigsize"].ToString());
                info.ug_pg_id = TypeConverter.StrToInt(dr["ug_pg_id"].ToString());
                info.ug_color = dr["ug_color"].ToString();
                info.ug_isSystem = TypeConverter.StrToInt(dr["ug_isSystem"].ToString());
                
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
    }
}
