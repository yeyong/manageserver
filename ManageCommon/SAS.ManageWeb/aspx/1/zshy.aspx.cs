using System;
using System.IO;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    /// <summary>
    /// 浙商黄页
    /// </summary>
    public class zshy : CompanyPage
    {
        #region 变量声明
        /// <summary>
        /// 行业类别
        /// </summary>
        protected DataTable cataloglist = new DataTable();
        /// <summary>
        /// 类别ID
        /// </summary>
        protected int catalogid = SASRequest.GetInt("cid", 0);
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-浙商黄页-企业首页";
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(forumpath + "images/jquery.cluetip.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jqueryFunc.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.cluetip-min.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#views\").ExtendClick(\"views1\",\"viewsnr\",\"i\");"
                    + "\r\n " + "jQuery('#put').find(\".zshynr1ce\").find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);

            if (catalogid == 0) cataloglist = Catalogs.GetAllCatalogBySort(2);
            else cataloglist = Catalogs.GetAllCatalogByPid(catalogid);
        }
    }
}
