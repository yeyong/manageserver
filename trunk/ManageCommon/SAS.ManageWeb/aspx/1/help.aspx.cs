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
        /// <summary>
        /// 当前帮助信息
        /// </summary>
        protected HelpInfo currenthelp = new HelpInfo();
        /// <summary>
        /// 帮助ID
        /// </summary>
        protected int helpid = SASRequest.GetInt("hid", 0);

        protected override void ShowPage()
        {
            int helpindex = 0;

            if (helpid == 0 && helptype.Rows.Count > 0) helpid = TypeConverter.ObjectToInt(helptype.Rows[0]["id"]);
            currenthelp = Helps.GetHelpInfo(helpid);

            if (currenthelp.Pid > 0) helpindex = currenthelp.Pid;
            else helpindex = helpid;
            helptype.PrimaryKey = new DataColumn[] { helptype.Columns["id"] };
            helpindex = helptype.Rows.IndexOf(helptype.Rows.Find(helpindex));

            pagetitle = currenthelp.Title;
            UpdateMetaInfo(currenthelp.Title + ",浙商帮助", Utils.CutString(Utils.RemoveHtml(currenthelp.Message), 0, 60), "");

            AddLinkCss(rooturl + "templates/" + templatepath + "/css/channels.css");

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery('#help').find(\"b\").eq(" + helpindex + ").next().slideDown();"
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
