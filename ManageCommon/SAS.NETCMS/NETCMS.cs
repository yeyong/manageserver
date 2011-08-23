﻿using System;
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
        /// <param name="classid">所在类别ID</param>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public static List<NewsContent> GetNewsList(string classid, int newscount, string ordercol, string ordertype)
        {
            return GetNewsList(classid, newscount, ordercol, ordertype, "");
        }

        /// <summary>
        /// 获得图片类新闻信息列表
        /// </summary>
        public static List<NewsContent> GetPicNewList(string classid, int newscount, string ordercol, string ordertype)
        {
            return GetNewsList(classid, newscount, ordercol, ordertype, "[NewsType]=1");
        }

        /// <summary>
        /// 获得新闻信息列表
        /// </summary>
        /// <param name="classid">所在类别ID</param>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        /// <param name="strWhere">条件</param>
        private static List<NewsContent> GetNewsList(string classid, int newscount, string ordercol, string ordertype, string strWhere)
        {
            return DTOProvider.GetNewsEntity(Data.DbProvider.GetInstance().GetNewsList(classid, newscount, ordercol, ordertype, strWhere));
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
