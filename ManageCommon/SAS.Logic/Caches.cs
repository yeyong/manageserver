using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;
using SAS.Data;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 缓存前台的一些界面HTML数据
    /// </summary>
    public class Caches
    {
        private static object lockHelper = new object();

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns>禁止列表</returns>
        public static List<IpInfo> GetBannedIpList()
        {
            List<IpInfo> list = SAS.Cache.SASCache.GetCacheService().RetrieveObject("/SAS/BannedIp") as List<IpInfo>;

            if (list == null)
            {
                list = Ips.GetBannedIpList();
                SAS.Cache.SASCache.GetCacheService().AddObject("/SAS/BannedIp", list);
            }
            return list;
        }

    }
}
