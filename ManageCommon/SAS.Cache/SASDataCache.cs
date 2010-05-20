using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.Caching;

namespace SAS.Cache
{
    /// <summary>
    /// 数据依赖缓存
    /// </summary>
    public class SASDataCache
    {
        private static volatile SASDataCache instance = null;
        private static object lockHelper = new object();
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

        private SASDataCache()
        {
            
        }

        public static SASDataCache GetCacheService()
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

        /// <summary>
        /// 删除数据缓存
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveDataCache(string keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return;
            }
            webCache.Remove(keys);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="keys">缓存key</param>
        /// <param name="o">缓存对象</param>
        /// <param name="dep">缓存依赖</param>
        public virtual void SetDataCache(string keys, object o, CacheDependency dep)
        {
            SetDataCache(keys, o, 0, dep);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="keys">缓存key</param>
        /// <param name="o">缓存对象</param>
        /// <param name="timeOut">过期时间（分钟）</param>
        /// <param name="dep">缓存依赖</param>
        public virtual void SetDataCache(string keys, object o, int timeOut, CacheDependency dep)
        {
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            if (timeOut > 0)
            {
                webCache.Insert(keys, o, dep, System.DateTime.Now.AddMinutes(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(keys, o, dep, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <returns>对象</returns>
        public object GetDataCache(string objId)
        {
            //return objectTable[objId];

            if (objId == null || objId.Length == 0)
            {
                return null;
            }

            return webCache.Get(objId);
        }

        //建立回调委托的一个实例
        public void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                    break;
                case CacheItemRemovedReason.Expired:
                    {
                        //CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(this.onRemove);

                        //webCache.Insert(key, val, null, System.DateTime.Now.AddMinutes(TimeOut),
                        //    System.Web.Caching.Cache.NoSlidingExpiration,
                        //    System.Web.Caching.CacheItemPriority.High,
                        //    callBack);
                        break;
                    }
                case CacheItemRemovedReason.Removed:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Underused:
                    {
                        break;
                    }
                default: break;
            }

            //如需要使用缓存日志,则需要使用下面代码
            //myLogVisitor.WriteLog(this,key,val,reason);

        }
    }
}
