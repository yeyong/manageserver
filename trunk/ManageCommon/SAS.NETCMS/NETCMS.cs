using System;
using System.Text;

using SAS.Entity;
using SAS.Cache;
using SAS.Common.Generic;
using SAS.NETCMS.Data;

namespace SAS.NETCMS
{
    /// <summary>
    /// 文章实体操作
    /// </summary>
    public class NETCMS
    {
        /// <summary>
        /// 获得新闻信息列表
        /// </summary>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public static List<NewsContent> GetNewsList(int newscount, string ordercol, string ordertype)
        {
            return DTOProvider.GetNewsEntity(Data.DbProvider.GetInstance().GetNewsList(newscount, ordercol, ordertype));
        }

        /// <summary>
        /// 获得有效新闻频道列表
        /// </summary>
        /// <returns></returns>
        public static List<PubClassInfo> GETNewsClassList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = "News_ClassList";
            List<PubClassInfo> classlist = cache.RetrieveObject(cachekey) as List<PubClassInfo>;

            if (classlist == null)
            {
                classlist = DTOProvider.GetNewsClassEntity(Data.DbProvider.GetInstance().GetNewsClassList());               
                cache.AddObject(cachekey, classlist);
            }

            return classlist;
        }

        /// <summary>
        /// 获得新闻栏目信息
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public static PubClassInfo GetNewsClassInfo(string classid)
        {
            foreach (PubClassInfo pi in GETNewsClassList())
            {
                if (pi.ClassID.Equals(classid))
                {
                    return pi;
                }
            }
            return null;
        }
    }
}
