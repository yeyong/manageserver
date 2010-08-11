using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Text;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;
using SAS.Taobao;

namespace SAS.Web.UI
{
    public class TaoSitemapPage : Page
    {
        private TaoBaoConfigInfo taoconfig = TaoBaoConfigs.GetConfig();

        public TaoSitemapPage()
        {
            System.Web.HttpContext.Current.Response.ContentType = "application/xml";
            System.Web.HttpContext.Current.Response.AppendHeader("Last-Modified", DateTime.Now.ToString("r"));
            System.Web.HttpContext.Current.Response.Write(GetTaoSiteMap());
            System.Web.HttpContext.Current.Response.End();
        }

        private string GetTaoSiteMap()
        {
            string sitemapstr = "";
            sitemapstr = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/TaoSiteMap") as string;
            if (sitemapstr == null)
            {
                StringBuilder sitemapBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                sitemapBuilder.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

                DataRow[] navslist = Navs.GetNavigationByPid(4);
                foreach (DataRow dr in navslist)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + dr["url"]);
                    sitemapBuilder.Append("    <priority>1.0</priority>");
                    sitemapBuilder.Append("  </url>");
                }

                sitemapBuilder.Append("  <url>");
                sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "category.html");
                sitemapBuilder.Append("  </url>");

                List<CategoryInfo> clist = TaoBaos.GetCategoryListByParentID(0);
                foreach (CategoryInfo cinfo in clist)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "chanels_" + cinfo.Cid + ".html");
                    sitemapBuilder.Append("  </url>");

                    List<CategoryInfo> subclist = TaoBaos.GetCategoryListByParentID(cinfo.Cid);
                    foreach (CategoryInfo subcinfo in subclist)
                    {
                        sitemapBuilder.Append("  <url>");
                        sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "goodslist-p-" + subcinfo.Cid + ".html");
                        sitemapBuilder.Append("  </url>");
                    }
                }

                List<TaoBaoTopicInfo> ttopics = TaoBaos.GetTaoBaoTopicList();
                foreach (TaoBaoTopicInfo ttinfo in ttopics)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "topicshow-" + ttinfo.Tid + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                List<ActivityInfo> actlist = Activities.GetTaoActivities();
                foreach (ActivityInfo ainfo in actlist)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "actshow-" + ainfo.Id + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                List<ShopDetailInfo> shoplist = TaoBaos.GetAllTaoBaoShops();
                foreach (ShopDetailInfo shopinfo in shoplist)
                {
                    sitemapBuilder.Append("  <url>");
                    sitemapBuilder.AppendFormat("    <loc>{0}</loc>", taoconfig.TaoDomain + "storeshow-" + shopinfo.sid + ".html");
                    sitemapBuilder.Append("  </url>");
                }

                sitemapBuilder.Append("</urlset>");
                sitemapstr = sitemapBuilder.ToString();

                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/TaoSiteMap", sitemapstr);
            }

            return sitemapstr;
        }
    }

    public class ZheSitemapPage : Page
    {
        private GeneralConfigInfo config = GeneralConfigs.GetConfig();

        public ZheSitemapPage()
        {
            System.Web.HttpContext.Current.Response.ContentType = "application/xml";
            System.Web.HttpContext.Current.Response.AppendHeader("Last-Modified", DateTime.Now.ToString("r"));
            System.Web.HttpContext.Current.Response.Write(Feeds.GetSASSitemap(config.Sitemapttl));
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
