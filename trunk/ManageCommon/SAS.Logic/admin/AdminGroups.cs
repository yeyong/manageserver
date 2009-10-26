using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 管理组操作类
    /// </summary>
    public class AdminGroups
    {
        /// <summary>
        /// 获得到指定管理组信息
        /// </summary>
        /// <returns>管理组信息</returns>
        public static AdminGroupInfo[] GetAdminGroupList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            AdminGroupInfo[] admingroupArray = cache.RetrieveObject("/SAS/AdminGroupList") as AdminGroupInfo[];
            if (admingroupArray == null)
            {
                admingroupArray = SAS.Data.DataProvider.AdminGroups.GetAdminGroupList();
                cache.AddObject("/SAS/AdminGroupList", admingroupArray);
            }
            return admingroupArray;
        }

        /// <summary>
        /// 获得到指定管理组信息
        /// </summary>
        /// <param name="admingid">管理组ID</param>
        /// <returns>组信息</returns>
        public static AdminGroupInfo GetAdminGroupInfo(int admingid)
        {
            // 如果管理组id大于0
            if (admingid > 0)
            {
                AdminGroupInfo[] admingroupArray = GetAdminGroupList();
                foreach (AdminGroupInfo admingroup in admingroupArray)
                {
                    // 如果存在该管理组则返回该组信息
                    if (admingroup.Admingid == admingid)
                        return admingroup;
                }
            }
            // 如果不存在该组则返回null
            return null;
        }

        /// <summary>
        /// 删除指定的管理组信息
        /// </summary>
        /// <param name="admingid">管理组ID</param>
        /// <returns>更改记录数</returns>
        public static int DeleteAdminGroupInfo(short admingid)
        {
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/AdminGroupList");
            return SAS.Data.DataProvider.AdminGroups.DeleteAdminGroupInfo(admingid);
        }

    }
}
