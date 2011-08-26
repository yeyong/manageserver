using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Logic;
using SAS.Taobao;
using SAS.Entity;
using SAS.Entity.Domain;
using System.Text;

public partial class itemsearch : TaoBaoPage
{
    protected List<TaobaokeItem> itemlistitems = new List<TaobaokeItem>();
    public string keyword = SASRequest.GetString("keyword");
    public int startmoney = SASRequest.GetInt("startmoney", 0);
    public int endmoney = SASRequest.GetInt("endmoney", 0);
    public int sortid = SASRequest.GetInt("sortid", 0);
    public int viewtype = SASRequest.GetInt("viewtype", 1);
    /// <summary>
    /// 当前页码
    /// </summary>
    public int pageid = SASRequest.GetInt("page", 1);
    /// <summary>
    /// 页面尺寸
    /// </summary>
    public int pagesize = 30;
    /// <summary>
    /// 公司总数
    /// </summary>
    public long itemcount = 0;
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
    public string searchkey = "";

    protected override void ShowPage()
    {
        string sortstr = "commissionRate_desc";

        switch (sortid)
        {
            case 1:
                sortstr = "commissionNum_desc";
                break;
            case 2:
                sortstr = "credit_desc";
                break;
            case 3:
                sortstr = "price_asc";
                break;
            default:
                sortstr = "commissionRate_desc";
                break;
        }

        string startmoneystr = startmoney > 0 ? startmoney.ToString() : "";
        string endmoneystr = endmoney > 0 ? endmoney.ToString() : "";
        try
        {
            itemlistitems = TaoBaos.GetItemList(-1, Utils.RemoveHtml(keyword.Trim()), startmoneystr, endmoneystr, "", "", "", "", "", "", pagesize, pageid, sortstr, out itemcount);
        }
        catch
        {
            AddErrLine("您的页面正在跳转！");
            SetMetaRefresh(2, string.Format("http://s8.taobao.com/search?cat=0&commend=1,2&s=0&sort=coefp&n=40&q={0}&tab=coefp&pid={1}&mode=23", HttpUtility.UrlEncode(searchkey, Encoding.GetEncoding("GB2312")), taobaoconfig.UserID, taobaoconfig.AppKey));
            return;
        }
        SetConditionAndPage();

        pagetitle = string.Format("{0}-{0}商品搜索结果{1}", keyword, pageid > 1 ? "(" + pageid.ToString() + ")" : "");
        seokeyword = string.Format("{0}商品搜索,{0}商品列表,{0}商品集合,{0}", keyword);
        seodescription = string.Format("{0}商品搜索结果,{0}商品导购与推荐。", keyword);
    }

    /// <summary>
    /// 设置查询条件以及分页
    /// </summary>
    private void SetConditionAndPage()
    {
        pagesize = TypeConverter.ObjectToInt(taobaoconfig.ItemPageSize, 30);
        //获取总页数
        pagecount = Convert.ToInt32(itemcount % pagesize == 0 ? itemcount / pagesize : itemcount / pagesize + 1);
        if (pagecount == 0) pagecount = 1;
        pageid = pageid < 1 ? 1 : pageid;
        pageid = pageid > pagecount ? pagecount : pageid;
        searchkey = Utils.UrlEncode(keyword).Replace("'", "%27");
        pagenumbers = Utils.GetTaoBaoItemPageNumbers(pageid, pagecount, string.Format("goodssearch-{0}-{1}-{2}-{3}-{4}.html", sortid, viewtype, startmoney, endmoney, searchkey), 10);

        prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
        nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
    }
}
