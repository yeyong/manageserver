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
    /// <summary>
    /// 帮助中心
    /// </summary>
    public class help : CompanyPage
    {
        /// <summary>
        /// 帮助类别
        /// </summary>
        protected DataTable helptype = Helps.GetHelpTypes();
        /// <summary>
        /// 帮助列表
        /// </summary>
        protected List<HelpInfo> helplist = Helps.GetAllHelpList();

        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery('#help').find(\"b:first\").next().slideDown();"
                    + "\r\n " + "jQuery('#help').find(\"b\").click(function() {"
                    + "\r\n " + "  var helpnr = jQuery(this).next();"
                    + "\r\n " + "  if (helpnr.is(':visible')) {helpnr.slideUp();jQuery(this).find(\"em\").html(\"+\");}"
                    + "\r\n " + "  else {jQuery('#help').find(\"b\").next().slideUp();jQuery('#help').find(\"b\").find(\"em\").html(\"+\");helpnr.slideDown();jQuery(this).find(\"em\").html(\"-\");}"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/diaocha.gif\",objhref:\"javascript:scrollTo(0,0)\"});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);
        }
    }
}
