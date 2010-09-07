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

public partial class itemlist : TaoBaoPage
{
    protected List<TaobaokeItem> itemlistitems = new List<TaobaokeItem>();
    protected List<CategoryInfo> itemlistcategories = new List<CategoryInfo>();
    protected List<GoodsBrandInfo> itemlistgoodsbrands = new List<GoodsBrandInfo>();
    protected List<TaobaokeItem> putitemlist = new List<TaobaokeItem>();
    /// <summary>
    /// 根类别
    /// </summary>
    protected CategoryInfo rootcategory = new CategoryInfo();
    /// <summary>
    /// 父级类别
    /// </summary>
    protected CategoryInfo parentcategory = new CategoryInfo();
    protected int cid = SASRequest.GetInt("cid", 0);
    protected int pid = SASRequest.GetInt("pid", 0);
    public string keyword = SASRequest.GetString("keyword");
    public int startmoney = SASRequest.GetInt("startmoney",0);
    public int endmoney = SASRequest.GetInt("endmoney", 0);
    public int sortid = SASRequest.GetInt("sortid", 0);
    public int viewtype = SASRequest.GetInt("viewtype", 1);
    public string searchkey = "";
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
    protected string currentcategoryname = "";

    protected override void ShowPage()
    {
        if (cid + pid <= 0)
        {
            AddErrLine("您的信息错误！");
            return;
        }
        if (cid > 0)
        {
            parentcategory = TaoBaos.GetCategoryInfoByCache(cid.ToString());
        }
        else if (pid > 0)
        {
            parentcategory = TaoBaos.GetCategoryInfoByCache(pid);
            if (parentcategory.Cg_relateclass.Split(',').Length > 0)
            {
                if (parentcategory.Cg_relateclass.Split(',')[0].Split('|').Length > 0)
                {
                    cid = TypeConverter.StrToInt(parentcategory.Cg_relateclass.Split(',')[0].Split('|')[0], 0);
                }
            }

            if (cid<= 0)
            {
                AddErrLine("您的信息错误！");
                return;
            }
        }

        if (parentcategory == null)
        {
            AddErrLine("您的信息错误！");
            return;
        }

        rootcategory = TaoBaos.GetCategoryInfoByCache(parentcategory.Parentid);
        if (rootcategory == null)
        {
            AddErrLine("您的信息错误！");
            return;
        }
        string tempstr = "," + parentcategory.Cg_relateclass;
        if (tempstr.Trim().Trim(',').Length > 2) currentcategoryname = Utils.CutString(tempstr, tempstr.IndexOf('|', tempstr.IndexOf("," + cid + "|", 0)) + 1, tempstr.IndexOf(',', tempstr.IndexOf('|', tempstr.IndexOf("," + cid + "|", 0))) - tempstr.IndexOf('|', tempstr.IndexOf("," + cid + "|", 0)) - 1);
        itemlistcategories = TaoBaos.GetCategoryListByParentID(rootcategory.Cid);
        itemlistgoodsbrands = TaoBaos.GetGoodsBrandListByClass(rootcategory.Cid);
        putitemlist = TaoBaos.GetRecommendProduct(Convert.ToInt16(TaoChanel.List), parentcategory.Cid);

        string sortstr = "commissionNum_desc";

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
                sortstr = "commissionNum_desc";
                break;
        }
        string startmoneystr = startmoney > 0 ? startmoney.ToString() : "";
        string endmoneystr = endmoney > 0 ? endmoney.ToString() : "";
        try
        {
            itemlistitems = TaoBaos.GetItemList(cid, Utils.RemoveHtml(keyword.Trim()), startmoneystr, endmoneystr, "", "", "", "", "", "", pagesize, pageid, sortstr, out itemcount);
        }
        catch
        {
            AddErrLine("您的页面正在跳转！");
            SetMetaRefresh(2, string.Format("http://search8.taobao.com/browse/search_auction.htm?q={0}&pid={1}&search_type=auction&commend=all&at_topsearch=1&unid={2}&cat={3}", searchkey, taobaoconfig.UserID, taobaoconfig.AppKey, cid));
            return;
        }
        SetConditionAndPage();

        string takestr = pid > 0 ? parentcategory.Name : currentcategoryname;
        pagetitle = string.Format("{0}-{0}商品列表{1}", takestr, pageid > 1 ? "(" + pageid.ToString() + ")" : "");
        seokeyword = string.Format("{0}商品搜索,{0}商品列表,{0}商品集合,{0}", takestr);
        seodescription = string.Format("{0}商品列表,{0}商品导购与推荐。{1}", takestr, pageid > 1 ? "(" + pageid.ToString() + ")" : "");
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
        pagenumbers = Utils.GetTaoBaoItemPageNumbers(pageid, pagecount, string.Format("goodslist-{0}-{1}-{2}-{3}-{4}-{5}.html", cid, sortid, viewtype, startmoney, endmoney, searchkey), 10);

        prevpage = pageid - 1 > 0 ? pageid - 1 : pageid;
        nextpage = pageid + 1 > pagecount ? pagecount : pageid + 1;
    }
}
