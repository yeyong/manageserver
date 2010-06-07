using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    /// <summary>
    /// 企业展示
    /// </summary>
    public class zsshow : CompanyPage
    {
        /// <summary>
        /// 企业详细信息
        /// </summary>
        protected Companys companyshowinfo = new Companys();
        protected int showenid = SASRequest.GetInt("enid", 0);
        /// <summary>
        /// 企业浏览次数
        /// </summary>
        protected int companyviews = 0;

        protected override void ShowPage()
        {
            companyshowinfo = Companies.GetCompanyInfo(showenid);

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/jquery.cluetip.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.cluetip-min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.ratingmin.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery('#put').find(\"span\").find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});"
                    + "\r\n " + "jQuery(\"input[type=text],textarea\").each(function(){"
                    + "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});"
                    + "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery('.starshow').rating({"
                    + "\r\n " + "  callback: function(value, link){"
                    + "\r\n " + "    if(typeof value == 'undefined'){jQuery('.fsw3').find('em').html(0);jQuery('input[name=scores]').val(0);}"
                    + "\r\n " + "    else{jQuery('.fsw3').find('em').html(value);jQuery('input[name=scores]').val(value);}"
                    + "\r\n " + "}});"
                    + "\r\n " + "jQuery('#swinf').find(\".tswan\").click(function(){"
                    + "\r\n " + "   var cssname = jQuery('#swinf').find(\"#swinfcot\").attr(\"class\");"
                    + "\r\n " + "   var arrowicon = 'arrow-icon2.gif';"
                    + "\r\n " + "   var showcss = 'tswnrxin';"
                    + "\r\n " + "   var showzi = '点击隐藏部分介绍';"
                    + "\r\n " + "   if (cssname == 'tswnrxin') {showcss = 'tswnr';arrowicon = 'arrow-icon1.gif';showzi = '点击查看全部介绍';}"
                    + "\r\n " + "   jQuery(this).html(\"<em><img src='templates/" + templatepath + "/images/icon/\"+arrowicon+\"'/></em>\"+showzi);"
                    + "\r\n " + "   jQuery('#swinf').find(\"#swinfcot\").removeClass().addClass(showcss);"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/diaocha.gif\",objhref:\"javascript:scrollTo(0,0)\"});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);

            CompaniesStats.Track(showenid, 1);
            companyviews = companyshowinfo.En_accesses + 1 + (config.TopicQueueStats == 1 ? CompaniesStats.GetStoredCompanyViewCount(companyshowinfo.En_id) : 0);
        }
    }
}
