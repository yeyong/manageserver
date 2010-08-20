﻿using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Config.Provider;

namespace SAS.Logic
{
    /// <summary>
    /// Feed操作类
    /// </summary>
    public class Feeds
    {
        private static GeneralConfigInfo config = GeneralConfigs.GetConfig();
        /// <summary>
        /// 获得Google收录协议xml
        /// </summary>
        /// <param name="ttl">TTL数值</param>
        public static string GetSASSitemap(int ttl)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string sitemap = cache.RetrieveObject("/SAS/Sitemap") as string;

            if (sitemap == null)
            {
                StringBuilder sitemapBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                sitemapBuilder.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

                sitemapBuilder.Append("  <url>");
                sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/index.html");
                sitemapBuilder.Append("    <priority>1.0</priority>");
                sitemapBuilder.Append("  </url>");
                sitemapBuilder.Append("  <url>");
                sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/zshy.html");
                sitemapBuilder.Append("    <priority>1.0</priority>");
                sitemapBuilder.Append("  </url>");
                sitemapBuilder.Append("  <url>");
                sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/zscard.html");
                sitemapBuilder.Append("  </url>");

                foreach (DataRow dr in Catalogs.GetAllCatalog().Rows)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/zshy-" + dr["id"] + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                foreach (DataRow dr in Companies.GetCompanyTableList().Select("[en_status] = 2 AND [en_visble] = 1"))
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/" + dr["en_id"] + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                foreach (DataRow dr in Activities.GetActivitiesCache().Rows)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/activity-" + dr["id"] + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                sitemapBuilder.Append("</urlset>");
                sitemap = sitemapBuilder.ToString();
                //声明新的缓存策略接口
                SAS.Cache.ICacheStrategy ics = new SitemapCacheStrategy();
                ics.TimeOut = ttl * 60;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/SAS/Sitemap", sitemap);
                cache.LoadDefaultCacheStrategy();
            }
            return sitemap;
        }
        /// <summary>
        /// 获得企业展示收录协议xml
        /// </summary>
        public static string GetShowSitemap(int ttl)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string sitemap = cache.RetrieveObject("/SAS/ShowSitemap") as string;
            if (sitemap == null)
            {
                StringBuilder sitemapBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                sitemapBuilder.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

                foreach (DataRow dr in Companies.GetCompanyTableList().Select("[en_status] = 2 AND [en_visble] = 1"))
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", config.Weburl + "/" + dr["en_id"] + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                sitemapBuilder.Append("</urlset>");
                sitemap = sitemapBuilder.ToString();
                //声明新的缓存策略接口
                SAS.Cache.ICacheStrategy ics = new SitemapCacheStrategy();
                ics.TimeOut = ttl * 60;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/SAS/ShowSitemap", sitemap);
                cache.LoadDefaultCacheStrategy();
            }
            return sitemap;
        }
    }
}
