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

public partial class index : TaoBaoPage
{
    /// <summary>
    /// 首页推荐品牌
    /// </summary>
    protected List<GoodsBrandInfo> ginfolist = new List<GoodsBrandInfo>();
    /// <summary>
    /// 首页推荐店铺
    /// </summary>
    protected List<ShopDetailInfo> shoplist = new List<ShopDetailInfo>();
    /// <summary>
    /// 首页淘宝专题
    /// </summary>
    protected List<TaoBaoTopicInfo> indextopiclist = new List<TaoBaoTopicInfo>();

    protected override void ShowPage()
    {
        pagetitle = "商之源";
        ginfolist = TaoBaos.GetGoodsBrandList(Convert.ToInt16(TaoChanel.Index), 0);
        shoplist = TaoBaos.GetTaoBaoShopListByRecommend(Convert.ToInt16(TaoChanel.Index), 0);
        indextopiclist = TaoBaos.GetTaoBaoTopicList(Convert.ToInt16(TaoChanel.Index));
    }
}
