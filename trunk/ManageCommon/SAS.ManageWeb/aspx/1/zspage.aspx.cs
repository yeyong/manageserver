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
        /// 收录总数
        /// </summary>
        protected int allcount = 0;
        /// <summary>
        /// 审核通过
        /// </summary>
        protected int passcount = 0;
        /// <summary>
        /// 今日收录
        /// </summary>
        protected int todaycount = 0; 
        /// <summary>
        /// 待审核数
        /// </summary>
        protected int waitcount = 0;

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-黄页频道";
            UpdateMetaInfo("浙商,浙商黄页,黄页频道,杭州企业,企业推广", "黄页频道-浙商黄页的企业推荐平台，推荐包括工业、商业服务、公共服务及社会组织等四类标准行业的浙江企业。", "");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/ScrollText.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#hylt\").Exchange({ MIDS: \"hyltit1\", CIDS: \"hyltcon\", timer: 5000, count: 2, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#bill\").Exchange({ MIDS: \"hyce1t\", CIDS: \"hyce1con\", timer: 5000, count: 3, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#hyindy1\").Exchange({ MIDS: \"hyindy1t\", CIDS: \"hyindy1con\", timer: 5000, count: 3, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#hyindy2\").Exchange({ MIDS: \"hyindy2t\", CIDS: \"hyindy2con\", timer: 5000, count: 3, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#hyindy3\").Exchange({ MIDS: \"hyindy3t\", CIDS: \"hyindy3con\", timer: 5000, count: 3, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#hyindy4\").Exchange({ MIDS: \"hyindy4t\", CIDS: \"hyindy4con\", timer: 5000, count: 3, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#hyindy2c1\").Scroll({line:1,speed:800,left:\"btn2\",right:\"btn1\",timer:\"3000\"});"
                    + "\r\n " + "jQuery(\"#hyindy2c2\").Scroll({line:1,speed:800,left:\"btn4\",right:\"btn3\",timer:\"3000\"});"
                    + "\r\n " + "jQuery(\"#hyindy2c3\").Scroll({line:1,speed:800,left:\"btn6\",right:\"btn5\",timer:\"3000\"});"
                    + "\r\n " + "jQuery(\"#hyindy2c4\").Scroll({line:1,speed:800,left:\"btn8\",right:\"btn7\",timer:\"3000\"});"
                    + "\r\n " + "var scrollup = new ScrollText(\"hyrtnr\");"
                    + "\r\n " + "scrollup.LineHeight = 23;"
                    + "\r\n " + "scrollup.Amount = 1;"
                    + "\r\n " + "scrollup.Timeout = 3000;"
                    + "\r\n " + "scrollup.Start();"
                    + "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});"
                    + "\r\n " + "});";
            AddfootScript(loadscript);

            Companies.GetCompanyCountSum(out allcount, out passcount, out todaycount, out waitcount);
        }
    }
}
