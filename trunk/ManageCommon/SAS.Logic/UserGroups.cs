using System;
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

    }
}
