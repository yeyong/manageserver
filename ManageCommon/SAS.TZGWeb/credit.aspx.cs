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
        pagetitle = "信誉店铺导购";
        classcount = classlist.Count;
    }

    protected SAS.Common.Generic.List<ShopDetailInfo> GetShopList(int classid)
    {
        return TaoBaos.GetTaoBaoShopListByRecommend(Convert.ToInt16(TaoChanel.Credit), classid);
    }
}
