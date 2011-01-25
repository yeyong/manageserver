using System;
using System.IO;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class inquiry : CompanyPage
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = SASRequest.GetInt("page", 1);
        /// <summary>
        /// get参数
        /// </summary>
        public string getParm = SASRequest.GetString("inqyname");
        /// <summary>
        /// 页面尺寸
        /// </summary>
        public int pagesize = 0;
        /// <summary>
        /// 公告总数
        /// </summary>
        public int announcecount = 0;
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
        /// 公司总数
        /// </summary>
        public int companycount = 0;
        /// <summary>
        /// 企业信誉排行
        /// </summary>
        protected List<Companys> creditcompanylist = Companies.GetCompanyListCredits();
        /// <summary>
        /// 点击企业排行
        /// </summary>
        protected List<Companys> companyaccesseslist = Companies.GetCompanyListViews();
        /// <summary>
        /// 企业评论排行
        /// </summary>
        protected List<Companys> commentcompanylist = Companies.GetCompanyListComments();
        /// <summary>
        /// 企业信息列表
        /// </summary>
        protected List<Companys> companylist = new List<Companys>();
        /// <summary>
        /// 查询条件
        /// </summary>
        private string condition = "";

        protected override void ShowPage()
        {
            pagetitle = "企业状态查询";
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            if (templateid == 2)
            {
                string loadscript = "\r\n " + "jQuery(document).ready(function() {";
                loadscript += "\r\n" + "jQuery(\"#ozs\").Exchange({ MIDS: \"lttit\", CIDS: \"ozsnr\", timer: 5000, count: 5, mousetype: 1 });";
                loadscript += "\r\n " + "});\r\n";
                AddfootScript(loadscript);
            }
            getParm = Utils.RemoveHtml(getParm.Trim());
            SetConditionAndPage();
            companylist = Companies.GetCompanyPageList(0, pageid, pagesize, 0, condition);
        }

        /// <summary>
        /// 设置查询条件以及分页
        /// </summary>
        private void SetConditionAndPage()
        {
            condition = Companies.GetCompanySearchCondition(true, getParm, -1, false, "", "", -1);
            companycount = Companies.GetCompanyCount(0, condition);
            pagesize = TypeConverter.ObjectToInt(config.Tpp, 0);
            //获取总页数
            pagecount = companycount % pagesize == 0 ? companycount / pagesize : companycount / pagesize + 1;
            if (pagecount == 0) pagecount = 1;
            pageid = pageid < 1 ? 1 : pageid;
            pageid = pageid > pagecount ? pagecount : pageid;

            pagenumbers = Utils.GetSASPageNumbers(pageid, pagecount, "inquiry.aspx?inqyname=" + Utils.UrlEncode(getParm), 10, "page", templateid);

            prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
            nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
        }
    }
}
