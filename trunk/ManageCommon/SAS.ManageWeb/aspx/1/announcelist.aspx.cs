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
    public class announcelist : CompanyPage
    {
        /// <summary>
        /// 公告列表
        /// </summary>
        public List<AnnouncementInfo> curannouncelist = new List<AnnouncementInfo>();
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = SASRequest.GetInt("page", 1);
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

        protected override void ShowPage()
        {
            pagetitle = "公告列表-浙商公告列表" + (pageid > 1 ? "(" + pageid.ToString() + ")" : "");
            UpdateMetaInfo(config.Seokeywords + "," + "浙商公告", "浙商黄页公告列表。" + config.Seodescription, "");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            SetAnnouncePage();
            curannouncelist = Announcements.GetAnnouncementList(pagesize, pageid);
        }

        /// <summary>
        /// 公告分页
        /// </summary>
        private void SetAnnouncePage()
        {
            announcecount = Announcements.GetAnnouncementCount();
            pagesize = 20;
            //获取总页数
            pagecount = announcecount % pagesize == 0 ? announcecount / pagesize : announcecount / pagesize + 1;
            if (pagecount == 0) pagecount = 1;
            pageid = pageid < 1 ? 1 : pageid;
            pageid = pageid > pagecount ? pagecount : pageid;
            pagenumbers = Utils.GetCompanyPageNumbers(pageid, pagecount, "nouncelist.html", 10, '.', templateid);

            prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
            nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
        }
    }
}
