using System;
using System.Text;
using System.Data;
using System.Web.Caching;

using SAS.Entity;
using SAS.Plugin.NETCMS;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Logic
{
    public class News
    {
        /// <summary>
        /// 获取最新每日资讯
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<NewsContent> GetHourNews()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "SAS_TodayNews";
            List<NewsContent> newslist = cache.RetrieveObject(cachekey) as List<NewsContent>;

            if (newslist == null)
            {
                newslist = NETCMSPluginProvider.GetInstance().GetNewsList("", 5, "id", "desc");
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 60;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, newslist);
                cache.LoadDefaultCacheStrategy();
            }

            return newslist == null ? new List<NewsContent>() : newslist;
        }

        /// <summary>
        /// 根据栏目获取资讯信息
        /// </summary>
        public static List<NewsContent> GetNewsByClass(string classid)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "SAS_News_" + classid;
            List<NewsContent> newslist = cache.RetrieveObject(cachekey) as List<NewsContent>;

            if (newslist == null)
            {
                newslist = NETCMSPluginProvider.GetInstance().GetNewsList(classid, 8, "id", "desc");
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 60;
                cache.LoadCacheStrategy(ica);
                cache.AddObject(cachekey, newslist);
                cache.LoadDefaultCacheStrategy();
            }

            return newslist == null ? new List<NewsContent>() : newslist;
        }

        /// <summary>
        /// 获取栏目访问路径
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public static PubClassInfo GetNewsClassInfo(string classid)
        {
            return NETCMSPluginProvider.GetInstance().GetClassUrl(classid);
        }
    }
}
