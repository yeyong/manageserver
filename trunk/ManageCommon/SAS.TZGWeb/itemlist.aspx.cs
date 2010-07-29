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
    public string startmoney = SASRequest.GetString("startmoney");
    public string endmoney = SASRequest.GetString("endmoney");
    public string startcredit = SASRequest.GetString("startcredit");
    public string endcredit = SASRequest.GetString("endcredit");
    public string startnum = SASRequest.GetString("startnum");
    public string endnum = SASRequest.GetString("endnum");
    public string sortstr = SASRequest.GetString("sortstr");

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
            itemlistcategories = TaoBaos.GetCategoryListByParentID(rootcategory.Cid);
        }
        else if (pid > 0)
        {

        }

        itemlistgoodsbrands = TaoBaos.GetGoodsBrandListByClass(rootcategory.Cid);

        itemlistitems = TaoBaos.GetItemList(cid, keyword, startmoney, endmoney, startcredit, endcredit, "", "", startnum, endnum, pagesize, pageid, sortstr, out itemcount);
    }
}
