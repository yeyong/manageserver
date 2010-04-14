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
        /// <summary>
        /// 省级ID
        /// </summary>
        protected int provinceid = SASRequest.GetInt("provinceid", 0);
        /// <summary>
        /// 市级ID
        /// </summary>
        protected int cityid = SASRequest.GetInt("cityid", 0);
        /// <summary>
        /// 地区ID
        /// </summary>
        protected int areaid = SASRequest.GetInt("areaid", 0);
        /// <summary>
        /// 页面导航
        /// </summary>
        protected string pagenav = " &gt; 浙商黄页";
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-浙商黄页-企业首页";
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(forumpath + "images/jquery.cluetip.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jqueryFunc.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/locations.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.cluetip-min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/template_catalogadmin.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#thelocation\").find('select').each(function(){jQuery(this).change(function(){LoadLocation({objid:'thelocation',provinceid:" + provinceid + ",cityid:" + cityid + ",areaid:" + areaid + ",urlparms:'cid=" + catalogid + "'});});});"
                    + "\r\n " + "jQuery(\"#views\").ExtendClick(\"views1\",\"viewsnr\",\"i\");"
                    + "\r\n " + "jQuery('#put').find(\".zshynr1ce\").find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);

            if (catalogid == 0) cataloglist = Catalogs.GetAllCatalogBySort(1);
            else
            {
                cataloglist = Catalogs.GetAllCatalogByPid(catalogid);
                CatalogInfo _cli = Catalogs.GetCatalogCacheInfo(catalogid);
                if (_cli != null)
                {
                    pagenav = " &gt; <a href=\"zshy.aspx\" title=\"浙商黄页\" class=\"l_666\">浙商黄页</a>";
                    foreach (string str in _cli.parentlist.Split(','))
                    {
                        CatalogInfo subcli = Catalogs.GetCatalogCacheInfo(TypeConverter.StrToInt(str, 0));
                        if (subcli == null) continue;
                        if (subcli.parentid == 0) continue;
                        pagenav += String.Format(" &gt; <a href=\"?cid={0}\" title=\"{1}\" class=\"l_666\">{1}</a>", subcli.id, subcli.name);
                    }
                    pagenav += " &gt; " + _cli.name;
                    pagetitle = "浙商黄页-浙商黄页-" + _cli.name;
                }
            }
        }
    }
}
