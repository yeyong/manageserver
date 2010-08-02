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

    protected override void ShowPage()
    {
        tkitem = TaoBaos.GetTaoBaoKeItemDetail(iid);
        iteminfo = tkitem.Item;
        tklocation = iteminfo.Location;

        if (tkitem == null)
        {
            AddErrLine("商品详请错误！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }
    }
}
