using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;

namespace SAS.Cache
{
    /// <summary>
    /// 数据依赖缓存
    /// </summary>
    public class SASDataCache
    {
        private static ICacheStrategy cs;
        private static volatile SASDataCache instance = null;
        private static object lockHelper = new object();

        private SASDataCache()
        {
            cs =new DefaultCacheStrategy();
        }

        public SASDataCache GetCacheService()
        {

            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new SASDataCache();
                    }
                }
            }

            return instance;
        }
    }
}
