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

public partial class itemshow : TaoBaoPage
{
    /// <summary>
    /// 商品ID
    /// </summary>
    protected long iid = long.Parse(SASRequest.GetString("iid"));
    /// <summary>
    /// 商品信息
    /// </summary>
    protected Item iteminfo = new Item();
    /// <summary>
    /// 商品推广信息
    /// </summary>
    protected TaobaokeItemDetail tkitem = new TaobaokeItemDetail();
    protected Location tklocation = new Location();
    protected CategoryInfo subcinfo = new CategoryInfo();
    protected CategoryInfo rootinfo = new CategoryInfo();
    protected ShopDetailInfo sdinfo = new ShopDetailInfo();
    /// <summary>
    /// 店铺名称
    /// </summary>
    protected string shopname = "";
    /// <summary>
    /// 店铺推荐商品
    /// </summary>
    protected string shopproducts = "";
    /// <summary>
    /// 店铺好评率
    /// </summary>
    protected string shopscore = "";
    /// <summary>
    /// 店铺链接地址
    /// </summary>
    protected string shopurl = "";
    /// <summary>
    /// 店铺地址
    /// </summary>
    protected string shopaddress = "";
    /// <summary>
    /// 同类商品
    /// </summary>
    protected List<TaobaokeItem> sameclassproducts = new List<TaobaokeItem>();

    protected override void ShowPage()
    {
        tkitem = TaoBaos.GetTaoBaoKeItemDetail(iid);
        if (tkitem == null)
        {
            AddErrLine("商品已过期或已下架！");
            SetMetaRefresh(2, rooturl);
            return;
        }
        iteminfo = tkitem.Item;
        tklocation = iteminfo.Location;

        subcinfo = TaoBaos.GetCategoryInfoByCache(iteminfo.Cid.ToString());

        if (subcinfo != null)
        {
            //AddErrLine("您的页面正在跳转，请稍等！");
            //SetMetaRefresh(2, tkitem.ClickUrl);
            //return;


            rootinfo = TaoBaos.GetCategoryInfoByCache(subcinfo.Parentid);
            sameclassproducts = TaoBaos.GetRecommendProduct(Convert.ToInt16(TaoChanel.Detail), subcinfo.Cid);
        }
        shopname = iteminfo.Nick;
        shopscore = "100";
        shopurl = tkitem.ShopClickUrl;
        shopaddress = tklocation.State + tklocation.City;
        sdinfo = TaoBaos.GetTaoBaoShopInfoByNick(iteminfo.Nick);

        if (sdinfo != null)
        {
            shopname = sdinfo.title;
            shopproducts = sdinfo.relategoods;
            shopscore = (decimal.Round(decimal.Parse((((double)sdinfo.good_num / (double)sdinfo.total_num) * 100).ToString()), 2)).ToString();
            shopurl = "storeshow-" + sdinfo.sid + ".html";
            shopaddress = sdinfo.shop_province + sdinfo.shop_city;
        }

        pagetitle = string.Format("{0}-{0}商品详细介绍", iteminfo.Title);
        seokeyword = string.Format("{0}介绍,{0},{1}", iteminfo.Title, shopname);
        seodescription = string.Format("{0}商品详细介绍。{1}。", iteminfo.Title, Utils.CutString(Utils.RemoveHtml(iteminfo.Desc), 60));

        string viewinfo = iid + "|" + Utils.UrlEncode(iteminfo.Title) + "|" + iteminfo.Price + "|" + iteminfo.PicUrl;
        string lastviewids = "," + Utils.GetCookie("goodviews") + ",";

        if (!lastviewids.Contains("," + viewinfo + ","))
        {
            if (Utils.GetCookie("goodviews").Split(',').Length > 10)
            {
                Utils.WriteCookie("goodviews", viewinfo + "," + Utils.GetCookie("goodviews").Remove(0, Utils.GetCookie("goodviews").Split(',')[0].Length), 1440);
            }
            else
            {
                Utils.WriteCookie("goodviews", viewinfo + "," + Utils.GetCookie("goodviews"), 1440);
            }
        }
        
    }
}
