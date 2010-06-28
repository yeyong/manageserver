using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    /// <summary>
    /// 推广页
    /// </summary>
    public class promotion : CompanyPage
    {
        /// <summary>
        /// 企业详细信息
        /// </summary>
        protected Companys companyshowinfo = new Companys();
        /// <summary>
        /// 企业ID
        /// </summary>
        protected int showenid = SASRequest.GetInt("enid", 0);
        /// <summary>
        /// 名片模板ID
        /// </summary>
        protected int cardtempid = 0;
        /// <summary>
        /// 配置文件id
        /// </summary>
        protected int cardconfigid = 0;
        /// <summary>
        /// flash文件路径
        /// </summary>
        protected string flashpath = "";

        protected override void ShowPage()
        {
            companyshowinfo = Companies.GetCompanyInfo(showenid);
            if (companyshowinfo == null)
            {
                AddErrLine("该企业信息不存在或已删除！");
                return;
            }
            if (companyshowinfo.En_visble != 1)
            {
                AddErrLine("企业已被关闭，请与管理员联系！");
            }
            if (page_err > 0) return;

            pagetitle = "浙商黄页|企业推广页面|" + companyshowinfo.En_name;
            string m_keyword = companyshowinfo.ProvinceName + "," + companyshowinfo.CityName + "," + companyshowinfo.DistrictName + "," + companyshowinfo.En_name + "," + companyshowinfo.En_mail + "," + companyshowinfo.En_main.Trim(',') + "," + config.Seokeywords;
            string m_desc = config.Seodescription + Utils.CutString(Utils.RemoveHtml(companyshowinfo.En_desc), 0, 60);
            UpdateMetaInfo(m_keyword.Trim().Trim(','), m_desc.Trim().Trim(','), "");

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            //AddLinkCss(forumpath + "templates/" + templatepath + "/float.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/common.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-exchange.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                     + "\r\n " + "jQuery(\"#prom1\").Exchange({ MIDS: \"pmt1rt1\", CIDS: \"pmt1rt2\", count: 5, mousetype: 0 });"
                     + "\r\n " + "jQuery(\"#prom2\").Exchange({ MIDS: \"pmt1rt3\", CIDS: \"pmt1rt4\", count: 5, mousetype: 0 });"
                     + "\r\n " + "jQuery(\"#prom3\").Exchange({ MIDS: \"pmt1rt5\", CIDS: \"pmt1rt6\", count: 5, mousetype: 0 });"
                     + "\r\n " + "jQuery(\"#prom4\").Exchange({ MIDS: \"pmt1rt7\", CIDS: \"pmt1rt8\", count: 5, mousetype: 0 });"
                     + "\r\n " + "jQuery(\"input[type=text],textarea\").each(function(){"
                     + "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});"
                     + "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
                     + "\r\n " + "});"
                     + "\r\n " + "});\r\n";
            AddfootScript(loadscript);

            if (companyshowinfo.Configid == 0) cardconfigid = 1;

            CardConfigInfo cci = CardConfigs.GetCardConfigCacheInfo(cardconfigid);
            if (cci == null) cci = CardConfigs.GetCardConfigCacheInfo(1);

            cardtempid = cci.tid;

            CardTemplateInfo cti = Templates.GetCardTemplateItem(cardtempid);
            if (cti == null)
            {
                AddErrLine("名片信息不存在！");
                return;
            }
            string[] curparm = cti.currentfile.Split('|');
            if (curparm.Length == 0) AddErrLine("参数传递错误！");
            if (!Utils.IsImgFilename(curparm[0])) AddErrLine("参数传递错误！");
            if (IsErr()) return;
            if (curparm.Length > 0 && curparm[1] != "")
            {
                string backfilename = string.Format(@"{0}cardtemplate/{1}/{2}", BaseConfigs.GetSitePath, cti.directory, curparm[1]);
                if (!File.Exists(Utils.GetMapPath(backfilename)))
                {
                    AddErrLine("flash名片模板文件不存在！");
                    return;
                }
                flashpath = backfilename;
            }
        }
    }
}
