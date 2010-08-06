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

public partial class credit : TaoBaoPage
{
    protected List<CategoryInfo> classlist = TaoBaos.GetCategoryListByParentID(0);
    protected int classcount = 0;
    /// <summary>
    /// 1号广告
    /// </summary>
    protected AdShowInfo[] adlist1 = Advertisements.GetAdsByType(1, AdType.TaoCredit);

    protected override void ShowPage()
    {
        pagetitle = "信誉铺-信誉店铺导购";
        seokeyword = "信誉,淘之购信誉铺,淘宝店铺,淘宝信誉店铺,网上商铺,皇冠店铺";
        seodescription = "淘宝店铺导购，提供高信誉高信用等级的店铺推荐。";
        classcount = classlist.Count;
    }

    protected SAS.Common.Generic.List<ShopDetailInfo> GetShopList(int classid)
    {
        return TaoBaos.GetTaoBaoShopListByRecommend(Convert.ToInt16(TaoChanel.Credit), classid);
    }
}
