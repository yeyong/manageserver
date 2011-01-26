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
    public class announcedetail : CompanyPage
    {
        protected AnnouncementInfo announceinfo = new AnnouncementInfo();
        protected int announceid = SASRequest.GetInt("anid", 0);
        protected DataRow[] activelist;
        protected string curactivetitle = "";
        protected int curactiveid = 0;
        /// <summary>
        /// 企业信誉排行
        /// </summary>
        protected List<Companys> creditcompanylist = Companies.GetCompanyListCredits();
        /// <summary>
        /// 广告位1
        /// </summary>
        protected string[] annad1 = Advertisements.GetZSRandomAd(1, AdType.InPostAd).Split('|');
        /// <summary>
        /// 广告位2
        /// </summary>
        protected string[] annad2 = Advertisements.GetZSRandomAd(2, AdType.InPostAd).Split('|');
        /// <summary>
        /// 广告位3
        /// </summary>
        protected string[] annad3 = Advertisements.GetZSRandomAd(3, AdType.InPostAd).Split('|');

        protected override void ShowPage()
        {
            announceinfo = Announcements.GetAnnouncement(announceid);

            if (announceinfo == null)
            {
                AddErrLine("无该公告信息！");
                return;
            }

            pagetitle = announceinfo.Title;
            UpdateMetaInfo(announceinfo.Title, config.Seodescription + Utils.CutString(Utils.RemoveHtml(announceinfo.Message), 0, 60), "");

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");

            string loadscript = "\r\n " + "jQuery(document).ready(function() {";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery(\"input[type=text],textarea\").each(function(){";
                loadscript += "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input5_soout\");});";
                loadscript += "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input5_soon\");});";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "});\r\n";
            AddfootScript(loadscript);

            activelist = Activities.GetActivityByIds(announceinfo.Relateactive);

            if (activelist.Length > 0)
            {
                curactiveid = TypeConverter.ObjectToInt(activelist[0]["id"], 0);
                curactivetitle = activelist[0]["atitle"].ToString();
            }
        }
    }
}
