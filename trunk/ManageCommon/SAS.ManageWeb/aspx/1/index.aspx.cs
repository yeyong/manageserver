using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class index : CompanyPage
    {
        /// <summary>
        /// 行业类别列表
        /// </summary>
        protected DataRow[] cataloglist1 = Catalogs.GetAllCatalogBySort(0);
        /// <summary>
        /// 城市企业信息
        /// </summary>
        protected List<Companys> companylistbycity = new List<Companys>();
        /// <summary>
        /// 友情链接列表
        /// </summary>
        protected DataTable friendlinklist = Caches.GetSASLinkList();
        /// <summary>
        /// 公告列表
        /// </summary>
        public List<AnnouncementInfo> announcementlist = Announcements.GetAnnouncementIndex();
        /// <summary>
        /// 新加入企业集合
        /// </summary>
        protected List<Companys> newcompanylist = Companies.GetNewCompanyList();
        /// <summary>
        /// 企业信誉排行
        /// </summary>
        protected List<Companys> creditcompanylist = Companies.GetCompanyListCredits();
        /// <summary>
        /// 企业评论排行
        /// </summary>
        protected List<Companys> commentcompanylist = Companies.GetCompanyListComments();
        /// <summary>
        /// 企业访问排行
        /// </summary>
        protected List<Companys> clickcompanylist = Companies.GetCompanyListViews();
        /// <summary>
        /// 生产型企业
        /// </summary>
        protected List<Companys> mancompanylist = new List<Companys>();
        /// <summary>
        /// 销售型企业
        /// </summary>
        protected List<Companys> selcompanylist = new List<Companys>();
        /// <summary>
        /// 首页活动信息集合
        /// </summary>
        protected List<ActivityInfo> indexactlist = Activities.GetIndexActivities();
        /// <summary>
        /// 帮助列表
        /// </summary>
        protected List<HelpInfo> helplist = Helps.GetIndexHelpList();
        /// <summary>
        /// 首页城市
        /// </summary>
        protected DataRow[] indexcity;
        /// <summary>
        /// 广告位1
        /// </summary>
        protected string[] indexad1 = Advertisements.GetZSRandomAd(1, AdType.IndexImageAd).Split('|');
        /// <summary>
        /// 广告位2
        /// </summary>
        protected string[] indexad2 = Advertisements.GetZSRandomAd(2, AdType.IndexImageAd).Split('|');
        /// <summary>
        /// 广告位3
        /// </summary>
        protected string[] indexad3 = Advertisements.GetZSRandomAd(3, AdType.IndexImageAd).Split('|');
        /// <summary>
        /// 广告位4
        /// </summary>
        protected string[] indexad4 = Advertisements.GetZSRandomAd(4, AdType.IndexImageAd).Split('|');
        /// <summary>
        /// 广告位5
        /// </summary>
        protected AdShowInfo[] indexad5 = Advertisements.GetZSAdsByType(5, AdType.IndexImageAd);
        /// <summary>
        /// 首页对联广告1
        /// </summary>
        protected string[] indexdouble1 = Advertisements.GetZSRandomAd(1, AdType.DoubleAd).Split('|');
        /// <summary>
        /// 首页对联广告2
        /// </summary>
        protected string[] indexdouble2 = Advertisements.GetZSRandomAd(2, AdType.DoubleAd).Split('|');
        /// <summary>
        /// 每日资讯
        /// </summary>
        protected List<NewsContent> newslist = News.GetHourNews();
        /// <summary>
        /// 栏目资讯
        /// </summary>
        protected List<NewsContent> newslistbyclass = new List<NewsContent>();
        /// <summary>
        /// 栏目信息
        /// </summary>
        protected PubClassInfo pclassinfo = new PubClassInfo();

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-商之源";

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/main.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/ScrollText.js\" type=\"text/javascript\"></script>";

            string adtempstr = "";
            if (indexdouble1.Length >= 8)
            {
                adtempstr += "\r\n " + "jQuery(this).Couplet({closeicon:\"templates/" + templatepath + "/images/cross.png\",layout:\"left\",distance:20,objsrc:\"" + indexdouble1[1] + "\",objhref:\"" + indexdouble1[4] + "\"})";
            }
            if (indexdouble2.Length >= 8)
            {
                adtempstr += "\r\n " + "jQuery(this).Couplet({closeicon:\"templates/" + templatepath + "/images/cross.png\",layout:\"right\",distance:20,objsrc:\"" + indexdouble2[1] + "\",objhref:\"" + indexdouble2[4] + "\"})";
            }

            string loadscript = "\r\n " + "jQuery(document).ready(function() {";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery(\"#bulletin\").find(\"ul:last\").hide();";
                loadscript += "\r\n " + "jQuery(\"#bulletin\").find(\"p:first\").mouseover(function() {";
                loadscript += "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").show();";
                loadscript += "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").hide();";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#bulletin\").find(\"p:last\").mouseover(function() {";
                loadscript += "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").show();";
                loadscript += "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").hide();";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#tao\").Exchange({ MIDS: \"onelt2tit\", CIDS: \"onelt2con\", count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#bill\").Exchange({ MIDS: \"onece1t\", CIDS: \"onece1con\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#prod\").Exchange({ MIDS: \"onert3t\", CIDS: \"onert3con\", count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#mcd\").find(\"li\").capslide({ caption_color: 'black', caption_bgcolor: 'white', overlay_bgcolor: '#e7dad8', border: '2px solid #e7dad8', showcaption: true });";
                loadscript += "\r\n " + "var scrollup = new ScrollText(\"listcomp\");";
                loadscript += "\r\n " + "scrollup.LineHeight = 30;";
                loadscript += "\r\n " + "scrollup.Amount = 2;";
                loadscript += "\r\n " + "scrollup.Start();";
            }
            else
            {
                loadscript += "\r\n " + "jQuery(\"#ocead\").Exchange({ MIDS: \"oceadt\", CIDS: \"oceadc\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#ort2\").Exchange({ MIDS: \"ortit\", CIDS: \"ortnr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hot\").Exchange({ MIDS: \"hotit\", CIDS: \"hotnr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#rank\").Exchange({ MIDS: \"ranktit\", CIDS: \"ranknr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#ocetu\").Scroll({line:1,speed:800,left:\"btn2\",right:\"btn1\",timer:\"3000\"});";
                loadscript += "\r\n " + "jQuery(\"#card\").find(\"li\").mouseover(function(){";
                loadscript += "\r\n " + "	jQuery(\"#card\").find(\"li\").removeClass().addClass(\"tcdli1\");";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"tcdli2\");";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#card\").find(\"li\").mouseout(function(){";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"tcdli1\");";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#type\").find(\"div\").mouseover(function(){";
                loadscript += "\r\n " + "	jQuery(\"#type\").find(\"div\").removeClass().addClass(\"typnr\");";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"typnrb\");";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#type\").find(\"div\").mouseout(function(){";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"typnr\");";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});" + adtempstr;
            loadscript += "\r\n " + "});";
            AddfootScript(loadscript);

            indexcity = areas.GetIndexCity();
            if (templateid == 1)
            {
                mancompanylist = Companies.GetCompanyListByType(EnTypeEnum.Manufacturer);
                selcompanylist = Companies.GetCompanyListByType(EnTypeEnum.Dealers);
            }
        }
    }
}
