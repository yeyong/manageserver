using System;
using System.Data;
using System.Data.Common;
using System.Text;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 用户组操作类
    /// </summary>
    public class UserGroups
    {
        /// <summary>
        /// 获得用户组数据
        /// </summary>
        /// <returns>用户组数据</returns>
        public static List<UserGroupInfo> GetUserGroupList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<UserGroupInfo> userGruopInfoList = cache.RetrieveObject("/SAS/UserGroupList") as List<UserGroupInfo>;

            if (userGruopInfoList == null)
            {
                userGruopInfoList = SAS.Data.DataProvider.UserGroups.GetUserGroupList();
                cache.AddObject("/SAS/UserGroupList", userGruopInfoList);
            }
            return userGruopInfoList;
        }

        /// <summary>
        /// 读取指定组的信息
        /// </summary>
        /// <param name="groupid">组id</param>
        /// <returns>组信息</returns>
        public static UserGroupInfo GetUserGroupInfo(int groupid)
        {
            List<UserGroupInfo> userGroupInfoList = GetUserGroupList();

            // 如果用户组id为7则为游客
            if (groupid == 7)
                return userGroupInfoList[6];

            for (int i = 0; i < userGroupInfoList.Count; i++)
            {
                if (userGroupInfoList[i].ug_id == groupid)
                    return userGroupInfoList[i];
            }

            // 如果查找不到则为游客
            return userGroupInfoList[6];
        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserGroupForDataTable()
        {
            return Data.DataProvider.UserGroups.GetUserGroupForDataTable();
        }

        /// <summary>
        /// 获取管理组列表
        /// </summary>
        /// <returns></returns>
        public static List<UserGroupInfo> GetAdminUserGroup()
        {
            List<UserGroupInfo> list = GetUserGroupList();
            List<UserGroupInfo> adminList = new List<UserGroupInfo>();
            foreach (UserGroupInfo userGroupInfo in list)
            {
                if (userGroupInfo.ug_pg_id != 0)
                    adminList.Add(userGroupInfo);
            }
            return adminList;
        }

        /// <summary>
        /// 获取积分用户组
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCreditUserGroup()
        {
            return Data.DataProvider.UserGroups.GetCreditUserGroup();
        }

        /// <summary>
        /// 更新用户组信息
        /// </summary>
        /// <param name="info">用户组信息</param>
        public static void UpdateUserGroup(UserGroupInfo info)
        {
            Data.DataProvider.UserGroups.UpdateUserGroup(info);
        }
    }
}
