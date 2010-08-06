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

public partial class woman : TaoBaoPage
{
    protected List<RecommendWithProduct> rwplist = new List<RecommendWithProduct>();
    /// <summary>
    /// 1号广告
    /// </summary>
    protected AdShowInfo[] adlist1 = Advertisements.GetAdsByType(1, AdType.TaoWomen);
    /// <summary>
    /// 2号广告
    /// </summary>
    protected AdShowInfo[] adlist2 = Advertisements.GetAdsByType(2, AdType.TaoWomen);

    protected override void ShowPage()
    {
        rwplist = TaoBaos.GetProductWithRecommend(Convert.ToInt16(TaoChanel.Woman));
        pagetitle = "女人馆-女人购物天堂";
        seokeyword = "女人馆,女人购物,潮流女人";
        seodescription = "女人馆，淘之购特色导购栏目，荟萃时下流行新品。";
    }
}
