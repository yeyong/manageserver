using System;
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
                ics.TimeOut = ttl;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/SAS/ShowSitemap", sitemap);
                cache.LoadDefaultCacheStrategy();
            }
            return sitemap;
        }
        /// <summary>
        /// 获取Rssxml
        /// </summary>
        /// <param name="ttl"></param>
        public static string GetRssXML(int ttl)
        {

            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string rssContent = cache.RetrieveObject("/SAS/RSSXML") as string;

            if (rssContent == null)
            {
                StringBuilder rssBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                rssBuilder.Append("<rss version=\"2.0\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\">\r\n");
                rssBuilder.Append("  <channel>\r\n");
                rssBuilder.Append("    <title>浙商黄页：浙江企业检索及网络自助推广平台</title>\r\n");
                rssBuilder.AppendFormat("    <link>{0}</link>", config.Weburl);
                rssBuilder.Append("    <language>en</language>\r\n");
                rssBuilder.Append("    <docs>http://blogs.law.harvard.edu/tech/rss</docs>\r\n");
                rssBuilder.Append("    <generator>www.zheshangonline.com</generator>\r\n");
                rssBuilder.Append("    <description>\r\n");
                rssBuilder.Append("      <![CDATA[ 浙商黄页 -- 浙商 网络名片 天狼星 自助推广 ]]>\r\n");
                rssBuilder.Append("    </description>\r\n");

                foreach (Companys cominfo in SAS.Data.DataProvider.Companies.GetCompanyListByOrder(20, "en_createdate", true))
                {
                    rssBuilder.Append("    <item>\r\n");
                    rssBuilder.AppendFormat("      <link>{0}</link>\r\n", config.Weburl.EndsWith("/") ? config.Weburl + cominfo.En_id + ".html" : config.Weburl + "/" + cominfo.En_id + ".html");
                    rssBuilder.AppendFormat("      <title><![CDATA[ {0} ]]></title>\r\n", cominfo.En_name);
                    rssBuilder.Append("    <author>浙商黄页</author>\r\n");
                    rssBuilder.Append("    <category>www.zheshangonline.com浙商黄页企业信息</category>\r\n");
                    rssBuilder.AppendFormat("    <guid>{0}</guid>\r\n", config.Weburl.EndsWith("/") ? config.Weburl + cominfo.En_id + ".html" : config.Weburl + "/" + cominfo.En_id + ".html");
                    rssBuilder.AppendFormat("    <pubDate>{0}</pubDate>\r\n", Utils.HtmlEncode(Convert.ToDateTime(cominfo.En_update).ToString("r").Trim()));
                    rssBuilder.Append("    <description>\r\n");
                    rssBuilder.AppendFormat("      <![CDATA[ <p><a title=\"{0}\" href=\"{1}\"><img style=\"border:0\" alt=\"{0}\" src=\"{2}\"/></a></p>\r\n{3} ]]>\r\n", cominfo.En_name, config.Weburl.EndsWith("/") ? config.Weburl + cominfo.En_id + ".html" : config.Weburl + "/" + cominfo.En_id + ".html", config.Weburl.EndsWith("/") ? config.Weburl + "showcardimg_" + cominfo.En_id + ".html" : config.Weburl + "/showcardimg_" + cominfo.En_id + ".html", Utils.HtmlEncode(Utils.ClearUBB(cominfo.En_desc)).Trim());
                    rssBuilder.Append("    </description>\r\n");
                    rssBuilder.Append("    </item>\r\n");
                }
                rssBuilder.Append("  </channel>\r\n");
                rssBuilder.Append("</rss>\r\n");
                rssContent = rssBuilder.ToString();
                SAS.Cache.ICacheStrategy ics = new RssCacheStrategy();
                ics.TimeOut = ttl;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/SAS/RSSXML", rssContent);
                cache.LoadDefaultCacheStrategy();
            }
            return rssContent;
        }
    }
}
