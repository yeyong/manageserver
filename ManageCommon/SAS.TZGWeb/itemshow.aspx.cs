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

    protected override void ShowPage()
    {
        tkitem = TaoBaos.GetTaoBaoKeItemDetail(iid);
        iteminfo = tkitem.Item;
        tklocation = iteminfo.Location;

        subcinfo = TaoBaos.GetCategoryInfoByCache(iteminfo.Cid.ToString());
        rootinfo = TaoBaos.GetCategoryInfoByCache(subcinfo.Parentid);
        if (tkitem == null)
        {
            AddErrLine("商品详请错误！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }

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
