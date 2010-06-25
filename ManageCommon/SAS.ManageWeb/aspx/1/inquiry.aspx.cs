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
    public class inquiry : CompanyPage
    {
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
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
        }
    }
}
