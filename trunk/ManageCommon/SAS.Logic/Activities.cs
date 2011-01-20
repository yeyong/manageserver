using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Caching;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 活动专题操作类
    /// </summary>
    public class Activities
    {
        /// <summary>
        /// 获得活动专题查询语句
        /// </summary>
        /// <param name="atype">专题类型</param>
        /// <param name="title">标题</param>
        /// <param name="keyword">关键字</param>
        /// <param name="startdate">活动开始时间</param>
        /// <param name="endtdate">活动结束时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static string GetActivitiesSearchConditions(int atype, string title, string keyword, DateTime startdate, DateTime endtdate, int status)
        {
            return SAS.Data.DataProvider.Activities.GetActivitiesSearchConditions(atype, title, keyword, startdate, endtdate, status);
        }

        /// <summary>
        /// 根据条件获取活动信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static DataTable GetActivitiesByConditions(string conditions)
        {
            return SAS.Data.DataProvider.Activities.GetActivitiesByConditions(conditions);
        }

        /// <summary>
        /// 获取活动信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ActivityInfo GetActivityInfo(int id)
        {
            return SAS.Data.DataProvider.Activities.GetActivityInfo(id);
        }

        /// <summary>
        /// 获取有效活动信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEnableActivities()
        {
            return SAS.Data.DataProvider.Activities.GetEnableActivities();
        }

        /// <summary>
        /// 缓存有效活动信息
        /// </summary>
        public static DataTable GetActivitiesCache()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_ACTIVITY;
            DataTable activelist = cache.RetrieveObject(cachekey) as DataTable;
            if (activelist == null)
            {
                activelist = GetEnableActivities();
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 1440;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, activelist);
                cache.LoadDefaultCacheStrategy();
            }
            return activelist;
        }

        /// <summary>
        /// 获取首页活动
        /// </summary>
        public static List<ActivityInfo> GetIndexActivities()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "/SAS/IndexAct";
            List<ActivityInfo> activelist = new List<ActivityInfo>();
            activelist = cache.RetrieveObject(cachekey) as List<ActivityInfo>;
            if (activelist == null)
            {
                activelist = Data.DataProvider.Activities.GetActvitiesByType(9, ActivityType.IndexActivity);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, activelist);
                cache.LoadDefaultCacheStrategy();
            }
            return activelist;
        }

        /// <summary>
        /// 淘之购相关活动
        /// </summary>
        public static System.Collections.Generic.List<ActivityInfo> GetTaoActivities()
        {
            System.Collections.Generic.List<ActivityInfo> actlist = new System.Collections.Generic.List<ActivityInfo>();
            actlist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/TaoActivities") as System.Collections.Generic.List<ActivityInfo>;
            if (actlist == null)
            {
                actlist = SAS.Data.DataProvider.Activities.GetActvitiesByType(6, ActivityType.TaobaoActivity);
                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/TaoActivities", actlist);
            }
            return actlist;
        }
        /// <summary>
        /// 获取黄页活动
        /// </summary>
        public static List<ActivityInfo> GetHYActivities()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "/SAS/HYAct";
            List<ActivityInfo> activelist = new List<ActivityInfo>();
            activelist = cache.RetrieveObject(cachekey) as List<ActivityInfo>;
            if (activelist == null)
            {
                activelist = Data.DataProvider.Activities.GetActvitiesByType(10, ActivityType.IndexActivity);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, activelist);
                cache.LoadDefaultCacheStrategy();
            }
            return activelist;
        }
        /// <summary>
        /// 获取黄页活动
        /// </summary>
        public static List<ActivityInfo> GetHYTaoActivities()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "/SAS/HYTaoAct";
            List<ActivityInfo> activelist = new List<ActivityInfo>();
            activelist = cache.RetrieveObject(cachekey) as List<ActivityInfo>;
            if (activelist == null)
            {
                activelist = Data.DataProvider.Activities.GetActvitiesByType(10, ActivityType.TaobaoActivity);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, activelist);
                cache.LoadDefaultCacheStrategy();
            }
            return activelist;
        }

        /// <summary>
        /// 根据ID集合获取活动信息集合
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static DataRow[] GetActivityByIds(string idlist)
        {
            if (string.IsNullOrEmpty(idlist)) return new DataRow[0];
            return GetActivitiesCache().Select("[id] IN (" + idlist + ")");
        }
    }
}
