using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// UserFactoryAdmin 的摘要说明。
    /// 后台用户信息操作管理类
    /// </summary>
    public class AdminUsers : Users
    {
        /// <summary>
        /// 删除指定用户的所有信息
        /// </summary>
        /// <param name="uid">指定的用户uid</param>
        /// <param name="delposts">是否删除帖子</param>
        /// <param name="delpms">是否删除短消息</param>
        /// <returns></returns>
        public static bool DelUserAllInf(int uid, bool delposts, bool delpms)
        {
            bool val = Data.DataProvider.Users.DeleteUser(uid, delposts, delpms);
            if (val)
                SASCache.GetCacheService().RemoveObject("/SAS/Statistics");

            return val;
        }
    }
}
