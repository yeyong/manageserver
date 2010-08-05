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

public partial class trend : TaoBaoPage
{
    protected List<RecommendWithProduct> rwplist = new List<RecommendWithProduct>();
    /// <summary>
    /// 1号广告
    /// </summary>
    protected AdShowInfo[] adlist1 = Advertisements.GetAdsByType(1, AdType.TaoTrend);
    /// <summary>
    /// 2号广告
    /// </summary>
    protected string adlist2 = Advertisements.GetTaoRandomAd(2, AdType.TaoTrend);
    /// <summary>
    /// 3号广告
    /// </summary>
    protected AdShowInfo[] adlist3 = Advertisements.GetAdsByType(3, AdType.TaoTrend);
    /// <summary>
    /// 4号广告
    /// </summary>
    protected string adlist4 = Advertisements.GetTaoRandomAd(4, AdType.TaoTrend);

    protected override void ShowPage()
    {
        rwplist = TaoBaos.GetProductWithRecommend(Convert.ToInt16(TaoChanel.Trend));
    }
}
