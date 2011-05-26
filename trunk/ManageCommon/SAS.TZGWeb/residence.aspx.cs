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

public partial class residence : TaoBaoPage
{
    /// <summary>
    /// 1号广告
    /// </summary>
    protected string adlist1 = Advertisements.GetTaoRandomAd(1, AdType.TaoResidence);
    /// <summary>
    /// 2号广告
    /// </summary>
    protected AdShowInfo[] adlist2 = Advertisements.GetAdsByType(2, AdType.TaoResidence);
    /// <summary>
    /// 3号广告
    /// </summary>
    protected AdShowInfo[] adlist3 = Advertisements.GetAdsByType(3, AdType.TaoResidence);

    protected override void ShowPage()
    {
        pagetitle = "宅男宅女-淘宝换装美容顾问";
        seokeyword = "换装,美容顾问,试衣间";
        seodescription = "";
    }
}
