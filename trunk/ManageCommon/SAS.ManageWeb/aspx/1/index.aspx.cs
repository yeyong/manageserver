using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
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
        /// 生产型企业
        /// </summary>
        protected List<Companys> mancompanylist = Companies.GetCompanyListByType(EnTypeEnum.Manufacturer);
        /// <summary>
        /// 销售型企业
        /// </summary>
        protected List<Companys> selcompanylist = Companies.GetCompanyListByType(EnTypeEnum.Dealers);
        /// <summary>
        /// 帮助列表
        /// </summary>
        protected List<HelpInfo> helplist = Helps.GetIndexHelpList();
        /// <summary>
        /// 首页城市
        /// </summary>
        protected DataRow[] indexcity;

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-商之源-首页";

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/main.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-exchange.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/ScrollText.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.capSlide.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"ul:last\").hide();"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"p:first\").mouseover(function() {"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").show();"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").hide();"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"p:last\").mouseover(function() {"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").show();"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").hide();"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(\"#tao\").Exchange({ MIDS: \"onelt2tit\", CIDS: \"onelt2con\", count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#bill\").Exchange({ MIDS: \"onece1t\", CIDS: \"onece1con\", timer: 5000, count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#prod\").Exchange({ MIDS: \"onert3t\", CIDS: \"onert3con\", count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#mcd\").find(\"li\").capslide({ caption_color: 'black', caption_bgcolor: 'white', overlay_bgcolor: '#e7dad8', border: '2px solid #e7dad8', showcaption: true });"
                    + "\r\n " + "var scrollup = new ScrollText(\"listcomp\");"
                    + "\r\n " + "scrollup.LineHeight = 30;"
                    + "\r\n " + "scrollup.Amount = 2;"
                    + "\r\n " + "scrollup.Start();"
                    + "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});"
                    + "\r\n " + "});";
            AddfootScript(loadscript);

            indexcity = areas.GetIndexCity();
        }
    }
}
