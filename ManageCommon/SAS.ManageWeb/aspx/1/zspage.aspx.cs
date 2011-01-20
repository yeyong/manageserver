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
    public class zspage : CompanyPage
    {
        /// <summary>
        /// 黄页活动信息集合
        /// </summary>
        protected List<ActivityInfo> hyactlist = Activities.GetHYActivities();
        /// <summary>
        /// 淘宝活动信息集合
        /// </summary>
        protected List<ActivityInfo> hytaoactlist = Activities.GetHYTaoActivities();
        /// <summary>
        /// 最新审核通过企业
        /// </summary>
        protected List<Companys> hyupdatecompanylist = Companies.GetUpdateCompanyList();
        /// <summary>
        /// 积分企业信息
        /// </summary>
        protected List<Companys> hyscoredcompanylist = Companies.GetScoredCompanyList();
        /// <summary>
        /// 信誉企业信息
        /// </summary>
        protected List<Companys> hycreditcompanylist = Companies.GetCompanyListCredits();
        /// <summary>
        /// 杭州城市企业信息
        /// </summary>
        protected List<Companys> hycitycompanylist = new List<Companys>();
        /// <summary>
        /// 名片信息
        /// </summary>
        protected List<Companys> hycardcompanylist = new List<Companys>();
        /// <summary>
        /// 首页城市
        /// </summary>
        protected DataRow[] indexcity;
        /// <summary>
        /// 行业类别列表
        /// </summary>
        protected DataRow[] cataloglist1 = Catalogs.GetAllCatalogBySort(0);
        protected TaoBaoConfigInfo tbconfig = TaoBaoConfigs.GetConfig();
        /// <summary>
        /// 广告1
        /// </summary>
        protected AdShowInfo[] cardad1 = Advertisements.GetZSAdsByType(1, AdType.CardPicAD);
        /// <summary>
        /// 广告位2
        /// </summary>
        protected string[] cardad2 = Advertisements.GetZSRandomAd(2, AdType.CardPicAD).Split('|');
        /// <summary>
        /// 广告位3
        /// </summary>
        protected string[] cardad3 = Advertisements.GetZSRandomAd(3, AdType.CardPicAD).Split('|');
        /// <summary>
        /// 广告位4
        /// </summary>
        protected string[] cardad4 = Advertisements.GetZSRandomAd(4, AdType.CardPicAD).Split('|');
        /// <summary>
        /// 广告位5
        /// </summary>
        protected string[] cardad5 = Advertisements.GetZSRandomAd(5, AdType.CardPicAD).Split('|');

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-黄页频道";
            UpdateMetaInfo("浙商,浙商黄页,黄页频道,杭州企业,企业推广", "黄页频道-浙商黄页的企业推荐平台，推荐包括工业、商业服务、公共服务及社会组织等四类标准行业的浙江企业。", "");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/ScrollText.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "jQuery(document).ready(function() {";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery(\"#hylt\").Exchange({ MIDS: \"hyltit1\", CIDS: \"hyltcon\", timer: 5000, count: 2, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#bill\").Exchange({ MIDS: \"hyce1t\", CIDS: \"hyce1con\", timer: 5000, count: 3, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hyindy1\").Exchange({ MIDS: \"hyindy1t\", CIDS: \"hyindy1con\", timer: 5000, count: 3, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hyindy2\").Exchange({ MIDS: \"hyindy2t\", CIDS: \"hyindy2con\", timer: 5000, count: 3, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hyindy3\").Exchange({ MIDS: \"hyindy3t\", CIDS: \"hyindy3con\", timer: 5000, count: 3, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hyindy4\").Exchange({ MIDS: \"hyindy4t\", CIDS: \"hyindy4con\", timer: 5000, count: 3, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#hyindy2c1\").Scroll({line:1,speed:800,left:\"btn2\",right:\"btn1\",timer:\"3000\"});";
                loadscript += "\r\n " + "jQuery(\"#hyindy2c2\").Scroll({line:1,speed:800,left:\"btn4\",right:\"btn3\",timer:\"3000\"});";
                loadscript += "\r\n " + "jQuery(\"#hyindy2c3\").Scroll({line:1,speed:800,left:\"btn6\",right:\"btn5\",timer:\"3000\"});";
                loadscript += "\r\n " + "jQuery(\"#hyindy2c4\").Scroll({line:1,speed:800,left:\"btn8\",right:\"btn7\",timer:\"3000\"});";
                loadscript += "\r\n " + "var scrollup = new ScrollText(\"hyrtnr\");";
                loadscript += "\r\n " + "scrollup.LineHeight = 23;";
                loadscript += "\r\n " + "scrollup.Amount = 1;";
                loadscript += "\r\n " + "scrollup.Timeout = 3000;";
                loadscript += "\r\n " + "scrollup.Start();";
            }
            else
            {
                loadscript += "jQuery(\"#ozso\").Exchange({ MIDS: \"ozsot\", CIDS: \"ozsoc\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#ozs\").Exchange({ MIDS: \"ozstit\", CIDS: \"ozsnr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#pinf\").Exchange({ MIDS: \"pinftit\", CIDS: \"pinfnr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#zsarea1\").Exchange({ MIDS: \"zsart1\", CIDS: \"zsarc1\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#zsarea2\").Exchange({ MIDS: \"zsart2\", CIDS: \"zsarc2\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#zsarea3\").Exchange({ MIDS: \"zsart3\", CIDS: \"zsarc3\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#zsarea4\").Exchange({ MIDS: \"zsart4\", CIDS: \"zsarc4\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "jQuery(\"#card1\").Scroll({line:1,speed:800,left:\"btn12\",right:\"btn11\",timer:\"5000\"});";
                loadscript += "\r\n " + "jQuery(\"#card2\").Scroll({line:1,speed:800,left:\"btn22\",right:\"btn21\",timer:\"5000\"});";
                loadscript += "\r\n " + "jQuery(\"#card3\").Scroll({line:1,speed:800,left:\"btn32\",right:\"btn31\",timer:\"5000\"});";
                loadscript += "\r\n " + "jQuery(\"#card4\").Scroll({line:1,speed:800,left:\"btn42\",right:\"btn41\",timer:\"5000\"});";
                loadscript += "\r\n " + "jQuery(\"#wlpf\").find(\"li\").mouseover(function(){";
                loadscript += "\r\n " + "	jQuery(\"#wlpf\").find(\"li\").removeClass().addClass(\"pinfc1\");";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"pinfc2\");";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery(\"#qyxy\").find(\"li\").mouseover(function(){";
                loadscript += "\r\n " + "	jQuery(\"#qyxy\").find(\"li\").removeClass().addClass(\"pinfc1\");";
                loadscript += "\r\n " + "	jQuery(this).removeClass().addClass(\"pinfc2\");";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});";
            loadscript += "\r\n " + "});";
            AddfootScript(loadscript);
            indexcity = areas.GetIndexCity();
        }
    }
}
