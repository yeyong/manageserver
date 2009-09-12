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
    }
}
