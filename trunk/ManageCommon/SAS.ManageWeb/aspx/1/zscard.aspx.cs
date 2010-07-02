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
    /// 企业名片列表
    /// </summary>
    public class zscard : CompanyPage
    {
        #region 变量声明
        /// <summary>
        /// 行业类别
        /// </summary>
        protected DataRow[] cataloglist;
        /// <summary>
        /// 点击企业排行
        /// </summary>
        protected SAS.Common.Generic.List<Companys> companyaccesseslist = Companies.GetCompanyListViews();
        /// <summary>
        /// 企业信息列表
        /// </summary>
        protected SAS.Common.Generic.List<Companys> companylist = new SAS.Common.Generic.List<Companys>();
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
        protected string pagenav = " &gt; 浙商名片";
        #endregion

        protected override void ShowPage()
        {
            string m_keyword = "浙商名片,浙商企业另类展示,{0}行业,{0}企业,{0}生产商,{0}销售商,{0}供应商,{0}搜索,{0}大全,{0}商机,{0}名片," + config.Seokeywords; ;  //meta关键字
            string m_content = "浙商名片(www.zheshangonline.com)浙商企业信息检索。大力扶持中小企业，中小型企业的摇篮，免费的{0}企业展示平台，让所有的网站都成为您企业的展示平台，更多服务尽在浙商黄页展示平台！" + config.Seodescription;  //meta内容描述
            pagetitle = "浙商名片|浙商企业另类展示";
            searchkey = keyword;
            keyword = Utils.UrlEncode(keyword).Replace("'", "%27");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/locations.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.capSlide.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/template_catalogadmin.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#thelocation\").LoadLocation({provinceid:" + provinceid + ",cityid:" + cityid + ",areaid:" + areaid + ",urlparms:'zscard-" + catalogid + "-{1}-{2}-{3}-" + entypeid + "-" + regyear + "-" + ordertype + "-" + keyword + ".html'});"
                    + "\r\n " + "jQuery(\"#views\").ExtendClick(\"views1\",\"viewsnr\",\"i\"," + ordertype + ");"
                    + "\r\n " + "jQuery('#zscard').find(\"dd\").capslide({ caption_color: 'black', caption_bgcolor: 'white', overlay_bgcolor: '#eee9e8', border: '0px solid #e7dad8', showcaption: true });"
                    + "\r\n " + "jQuery('#zscard').find(\"input[type=text],textarea\").each(function(){"
                    + "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});"
                    + "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});"
                    + "\r\n " + "});\r\n";
            AddfootScript(loadscript);
            SetConditionAndPage();

            cataloglist = Catalogs.GetAllCatalogByPid(catalogid);
            CatalogInfo _cli = Catalogs.GetCatalogCacheInfo(catalogid);
            if (_cli != null)
            {
                pagenav = " &gt; <a href=\"zscard.html\" title=\"浙商名片\" class=\"l_666\">浙商名片</a>";
                foreach (string str in _cli.parentlist.Split(','))
                {
                    CatalogInfo subcli = Catalogs.GetCatalogCacheInfo(TypeConverter.StrToInt(str, 0));
                    if (subcli == null) continue;
                    //if (subcli.parentid == 0) continue;
                    pagenav += String.Format(" &gt; <a href=\"zscard-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}.html\" title=\"{0}\" class=\"l_666\">{0}</a>", subcli.name, subcli.id, provinceid, cityid, areaid, entypeid, regyear, ordertype, keyword);
                }
                pagenav += " &gt; " + _cli.name;
                pagetitle = pagetitle + "-" + _cli.name + "企业信息";
                m_keyword += string.Format(m_keyword, _cli.name);
                m_content += string.Format(m_content, _cli.name);
            }
            else
            {
                m_keyword += string.Format(m_keyword, "中小企业");
                m_content += string.Format(m_content, "中小企业");
            }
            UpdateMetaInfo(m_keyword, m_content, "");
            companylist = Companies.GetCompanyPageList(catalogid, pageid, pagesize, ordertype, condition);
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

            pagenumbers = Utils.GetCompanyPageNumbers(pageid, pagecount, string.Format("zscard-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}.html", catalogid, provinceid, cityid, areaid, entypeid, regyear, ordertype, keyword), 10);

            prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
            nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
        }
    }
}
