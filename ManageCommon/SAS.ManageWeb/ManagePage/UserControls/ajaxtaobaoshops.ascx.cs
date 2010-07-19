using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class ajaxtaobaoshops : System.Web.UI.UserControl
    {
        protected List<ShopDetailInfo> shoplist = new List<ShopDetailInfo>();
        protected TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        public string pagelink;
        public int currentpage = 0;
        //页面大小
        public int pagesize = 16;
        protected DisplayMode dmode = DisplayMode.SearchMode;

        public string shoptitle = SASRequest.GetString("shoptitle").Trim();
        public string shopnick = SASRequest.GetString("shopnick").Trim();
        public string province = SASRequest.GetString("province").Trim();
        public string city = SASRequest.GetString("city").Trim();
        public int startscore = SASRequest.GetInt("startscore",-1);
        public int endscore = SASRequest.GetInt("endscore", -1);
        public int startcredit = SASRequest.GetInt("startcredit", -1);
        public int endcredit = SASRequest.GetInt("endcredit", -1);
        public int startrate = SASRequest.GetInt("startrate", -1);
        public int endrate = SASRequest.GetInt("endrate", -1);
        public string ordercolumn = SASRequest.GetString("ordercolumn");
        public string ordertype = SASRequest.GetString("ordertype");
        public int display = SASRequest.GetInt("display", -1);

        /// <summary>
        /// 显示模式
        /// </summary>
        public DisplayMode Display
        {
            set { dmode = value; }
            get { return dmode; }
        }

        public ajaxtaobaoshops()
        {
            if (display == 1) dmode = DisplayMode.ManageMode;
            currentpage = SASRequest.GetInt("currentpage", 1);
            //获取当前页数
            if (SASRequest.GetInt("postnumber", 0) > 0)
            {
                pagesize = SASRequest.GetInt("postnumber", 0);
            }
            string conditions = tpb.GetTaoBaoShopCondition(shoptitle, shopnick, province, city, startscore, endscore, startcredit, endcredit, startrate, endrate);
            int recordcount = tpb.GetTaoBaoShopCountByCondition(conditions);
            shoplist = tpb.GetTaoBaoShopsPage(conditions, pagesize, currentpage, ordercolumn, ordertype);
            pagelink = AjaxPagination(recordcount, pagesize, currentpage);
        }

        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="currentpage">当前页数</param>
        public string AjaxPagination(int recordcount, int pagesize, int currentpage)
        {
            if (SASRequest.GetInt("postnumber", 0) > 0)
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtaobaoshops.ascx", "shoptitle=" + shoptitle + "&shopnick=" + shopnick + "&province=" + province + "&city=" + city + "&startscore=" + startscore + "&endscore=" + endscore + "&startcredit=" + startcredit + "&endcredit=" + endcredit + "&startrate=" + startrate + "&endrate=" + endrate + "&ordercolumn=" + ordercolumn + "&ordertype=" + ordertype + "&postnumber=" + SASRequest.GetInt("postnumber", 0), "taobaoshoplistgrid");
            }
            else
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtaobaoshops.ascx", "shoptitle=" + shoptitle + "&shopnick=" + shopnick + "&province=" + province + "&city=" + city + "&startscore=" + startscore + "&endscore=" + endscore + "&startcredit=" + startcredit + "&endcredit=" + endcredit + "&startrate=" + startrate + "&endrate=" + endrate + "&ordercolumn=" + ordercolumn + "&ordertype=" + ordertype, "taobaoshoplistgrid");
            }
        }

        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="currentpage">当前页数</param>
        public string AjaxPagination(int recordcount, int pagesize, int currentpage, string usercontrolname, string paramstr, string divname)
        {
            int allcurrentpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string currentpagestr = "<BR />";

            if (currentpage < 1)
            {
                currentpage = 1;
            }

            //计算总页数
            if (pagesize != 0)
            {
                allcurrentpage = (int)(recordcount / pagesize);
                allcurrentpage = ((recordcount % pagesize) != 0 ? allcurrentpage + 1 : allcurrentpage);
                allcurrentpage = (allcurrentpage == 0 ? 1 : allcurrentpage);
            }
            next = currentpage + 1;
            pre = currentpage - 1;

            //中间页起始序号
            startcount = (currentpage + 5) > allcurrentpage ? allcurrentpage - 9 : currentpage - 4;

            //中间页终止序号
            endcount = currentpage < 5 ? 10 : currentpage + 5;

            //为了避免输出的时候产生负数，设置如果小于1就从序号1开始
            if (startcount < 1)
            {
                startcount = 1;
            }

            //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内
            if (allcurrentpage < endcount)
            {
                endcount = allcurrentpage;
            }

            if (startcount > 1)
            {
                currentpagestr += currentpage > 1 ? "&nbsp;&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + pre + "');\" title=\"上一页\">上一页</a>" : "";
            }

            //当页码数大于1时, 则显示页码
            if (endcount > 1)
            {
                //中间页处理, 这个增加时间复杂度，减小空间复杂度
                for (int i = startcount; i <= endcount; i++)
                {
                    currentpagestr += currentpage == i ? "&nbsp;" + i + "" : "&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + i + "');\">" + i + "</a>";
                }
            }

            if (endcount < allcurrentpage)
            {
                currentpagestr += currentpage != allcurrentpage ? "&nbsp;&nbsp;<a href=\"###\" onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + next + "');\" title=\"下一页\">下一页</a>&nbsp;&nbsp;" : "";
            }

            if (endcount > 1)
            {
                currentpagestr += "&nbsp; &nbsp; &nbsp; &nbsp;";
            }

            currentpagestr += "共 " + allcurrentpage + " 页, 当前第 " + currentpage + " 页, 共 " + recordcount + " 条记录";

            return currentpagestr;

        }

        /// <summary>
        /// 显示模式
        /// </summary>
        public enum DisplayMode
        {
            /// <summary>
            /// 搜索模式
            /// </summary>
            SearchMode,
            /// <summary>
            /// 管理模式
            /// </summary>
            ManageMode
        }
    }
}