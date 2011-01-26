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
            string m_keyword = companyshowinfo.En_name + "," + companyshowinfo.En_main.Trim(',');
            string m_desc = config.Seodescription + companyshowinfo.ProvinceName + "," + companyshowinfo.CityName + "," + companyshowinfo.DistrictName + "。" + Utils.CutString(Utils.RemoveHtml(companyshowinfo.En_desc), 0, 60);
            UpdateMetaInfo(m_keyword.Trim().Trim(','), m_desc.Trim().Trim(','), "");

            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/sascommon.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-exchange.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {";
            loadscript += "\r\n " + "promotion_0 = document.getElementById('html_pro1').innerHTML;";
            loadscript += "\r\n " + "promotion_1 = document.getElementById('html_pro1').innerHTML;";
            loadscript += "\r\n " + "promotion_2 = document.getElementById('html_pro2').innerHTML;";
            loadscript += "\r\n " + "promotion_3 = document.getElementById('html_pro3').innerHTML;";
            loadscript += "\r\n " + "jQuery(\"#prom1\").Exchange({ MIDS: \"pmt1rt1\", CIDS: \"pmt1rt2\", count: 5, mousetype: 0 });";
            loadscript += "\r\n " + "jQuery(\"#prom2\").Exchange({ MIDS: \"pmt1rt3\", CIDS: \"pmt1rt4\", count: 5, mousetype: 0 });";
            loadscript += "\r\n " + "jQuery(\"#prom3\").Exchange({ MIDS: \"pmt1rt5\", CIDS: \"pmt1rt6\", count: 5, mousetype: 0 });";
            loadscript += "\r\n " + "jQuery(\"#prom4\").Exchange({ MIDS: \"pmt1rt7\", CIDS: \"pmt1rt8\", count: 5, mousetype: 0 });";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery('#promotioncontent').find(\"input[type=text],textarea\").each(function(){";
                loadscript += "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});";
                loadscript += "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "});\r\n";
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
