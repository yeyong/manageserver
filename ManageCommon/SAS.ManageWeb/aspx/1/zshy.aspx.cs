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
        /// 企业信息列表
        /// </summary>
        protected DataRow[] companylist;
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
        /// 企业类型
        /// </summary>
        protected int entypeid = SASRequest.GetInt("entypeid", 0);
        /// <summary>
        /// 注册年限
        /// </summary>
        protected int regyear = SASRequest.GetInt("regyear", 0);
        /// <summary>
        /// 排序
        /// </summary>
        protected int ordertype = SASRequest.GetInt("ordertype", 0);
        /// <summary>
        /// 搜索关键字
        /// </summary>
        protected string keyword = SASRequest.GetString("keyword");
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = SASRequest.GetInt("page", 1);
        /// <summary>
        /// 页面尺寸
        /// </summary>
        public int pagesize = 0;
        /// <summary>
        /// 公司总数
        /// </summary>
        public int companycount = 0;
        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 1;
        /// <summary>
        /// 上一页
        /// </summary>
        public int prevpage = 1;
        /// <summary>
        /// 下一页
        /// </summary>
        public int nextpage = 1;
        /// <summary>
        /// 指定最大查询数
        /// </summary>
        private int maxseachnumber = 10000;
        /// <summary>
        /// 页码链接
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// 搜索关键字
        /// </summary>
        private string searchkey = "";
        /// <summary>
        /// 地区列表
        /// </summary>
        private string arealist = "";
        /// <summary>
        /// 查询条件
        /// </summary>
        private string condition = "";
        /// <summary>
        /// 页面导航
        /// </summary>
        protected string pagenav = " &gt; 浙商黄页";
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-浙商黄页-企业首页";
            searchkey = keyword;
            keyword = Utils.UrlEncode(keyword).Replace("'", "%27");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/jquery.cluetip.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jqueryFunc.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/locations.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.cluetip-min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/template_catalogadmin.js\" type=\"text/javascript\"></script>";            

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#thelocation\").LoadLocation({provinceid:" + provinceid + ",cityid:" + cityid + ",areaid:" + areaid + ",urlparms:'zshy-" + catalogid + "-{1}-{2}-{3}-" + entypeid + "-" + regyear + "-" + ordertype + "-" + keyword + ".html'});"
                    + "\r\n " + "jQuery(\"#views\").ExtendClick(\"views1\",\"viewsnr\",\"i\"," + ordertype + ");"
                    + "\r\n " + "jQuery('#put').find(\".zshynr1ce\").find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});"
                    + "\r\n " + "jQuery(\"input[type=text],textarea\").each(function(){"
			        + "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});"
			        + "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
		            + "\r\n " + "});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);            
            SetConditionAndPage();

            if (catalogid == 0) cataloglist = Catalogs.GetAllCatalogBySort(1, Companies.GetCompanyCondition(arealist, entypeid, regyear, keyword));
            else
            {
                cataloglist = Catalogs.GetAllCatalogByPid(catalogid, Companies.GetCompanyCondition(arealist, entypeid, regyear, keyword));
                CatalogInfo _cli = Catalogs.GetCatalogCacheInfo(catalogid);
                if (_cli != null)
                {
                    pagenav = " &gt; <a href=\"zshy.html\" title=\"浙商黄页\" class=\"l_666\">浙商黄页</a>";
                    foreach (string str in _cli.parentlist.Split(','))
                    {
                        CatalogInfo subcli = Catalogs.GetCatalogCacheInfo(TypeConverter.StrToInt(str, 0));
                        if (subcli == null) continue;
                        if (subcli.parentid == 0) continue;
                        pagenav += String.Format(" &gt; <a href=\"zshy-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}.html\" title=\"{0}\" class=\"l_666\">{0}</a>", subcli.name, subcli.id, provinceid, cityid, areaid, entypeid, regyear, ordertype, keyword);
                    }
                    pagenav += " &gt; " + _cli.name;
                    pagetitle = "浙商黄页-浙商黄页-" + _cli.name;
                }
            }           

            companylist = Companies.GetCompanyPageList(catalogid, pageid, pagesize, "en_accesses", ordertype == 0 ? "desc" : "", condition);
        }

        /// <summary>
        /// 设置查询条件以及分页
        /// </summary>
        private void SetConditionAndPage()
        {
            if (areaid > 0) arealist = areaid.ToString();
            else if (cityid > 0) arealist = areas.GetDistrictIDByCity(cityid);
            else if (provinceid > 0) arealist = areas.GetDistrictIDByProvince(provinceid);
            condition = Companies.GetCompanyCondition(arealist, entypeid, regyear, searchkey);
            companycount = Companies.GetCompanyCount(catalogid, condition);
            pagesize = TypeConverter.ObjectToInt(config.Tpp, 0);
            //获取总页数
            pagecount = companycount % pagesize == 0 ? companycount / pagesize : companycount / pagesize + 1;
            if (pagecount == 0) pagecount = 1;
            pageid = pageid < 1 ? 1 : pageid;
            pageid = pageid > pagecount ? pagecount : pageid;

            pagenumbers = Utils.GetCompanyPageNumbers(pageid, pagecount, string.Format("zshy-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}.html", catalogid, provinceid, cityid, areaid, entypeid, regyear, ordertype, keyword), 10);

            prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
            nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
        }
    }
}
