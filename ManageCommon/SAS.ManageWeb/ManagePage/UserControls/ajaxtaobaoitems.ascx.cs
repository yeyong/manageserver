﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity.Domain;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class ajaxtaobaoitems : System.Web.UI.UserControl
    {
        protected System.Collections.Generic.List<TaobaokeItem> taobaoitemlist = new System.Collections.Generic.List<TaobaokeItem>();
        public string pagelink;
        public int currentpage = 0;
        public string itemname = "";
        public int cid = SASRequest.GetInt("cid", 0);
        public string keyword = SASRequest.GetString("keyword");
        public string startmoney = SASRequest.GetString("startmoney");
        public string endmoney = SASRequest.GetString("endmoney");
        public string startcredit = SASRequest.GetString("startcredit");
        public string endcredit = SASRequest.GetString("endcredit");
        public string startrate = SASRequest.GetString("startrate");
        public string endrate = SASRequest.GetString("endrate");
        public string startnum = SASRequest.GetString("startnum");
        public string endnum = SASRequest.GetString("endnum");
        public string sortstr = SASRequest.GetString("sortstr");
        //页面大小
        public int pagesize = 16;
        protected TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();

        public ajaxtaobaoitems()
        {
            currentpage = SASRequest.GetInt("currentpage", 1);
            //获取当前页数
            if (SASRequest.GetInt("postnumber", 0) > 0)
            {
                pagesize = SASRequest.GetInt("postnumber", 0);
            }
            long recordcount = 0;
            taobaoitemlist = tpb.GetItemListByCondition(cid, keyword, startmoney, endmoney, startcredit, endcredit, startrate, endrate, startnum, endnum, pagesize, currentpage, sortstr, out recordcount);
            pagelink = AjaxPagination(recordcount, 12, currentpage);
        }

        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="currentpage">当前页数</param>
        public string AjaxPagination(long recordcount, int pagesize, int currentpage)
        {
            if (SASRequest.GetInt("postnumber", 0) > 0)
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtaobaoitems.ascx", "cid=" + cid + "&keyword=" + keyword + "&startmoney=" + startmoney + "&endmoney=" + endmoney + "&startcredit=" + startcredit + "&endcredit=" + endcredit + "&startrate=" + startrate + "&endrate=" + endrate + "&startnum=" + startnum + "&endnum=" + endnum + "&sortstr=" + sortstr + "&postnumber=" + SASRequest.GetInt("postnumber", 0), "taobaoitemlistgrid");
            }
            else
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtaobaoitems.ascx", "cid=" + cid + "&keyword=" + keyword + "&startmoney=" + startmoney + "&endmoney=" + endmoney + "&startcredit=" + startcredit + "&endcredit=" + endcredit + "&startrate=" + startrate + "&endrate=" + endrate + "&startnum=" + startnum + "&endnum=" + endnum + "&sortstr=" + sortstr, "taobaoitemlistgrid");
            }
        }

        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="currentpage">当前页数</param>
        public string AjaxPagination(long recordcount, int pagesize, int currentpage, string usercontrolname, string paramstr, string divname)
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
    }
}