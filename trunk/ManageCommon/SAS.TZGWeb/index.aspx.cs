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
    /// <summary>
    /// 活动列表
    /// </summary>
    protected List<ActivityInfo> taoactlist = new List<ActivityInfo>();
    /// <summary>
    /// 友情链接列表
    /// </summary>
    protected List<FriendLinkInfo> flinklist = new List<FriendLinkInfo>();
    /// <summary>
    /// 1号广告
    /// </summary>
    protected AdShowInfo[] adlist1 = Advertisements.GetAdsByType(1, AdType.TaoIndexAD);
    /// <summary>
    /// 2号广告
    /// </summary>
    protected string adlist2 = Advertisements.GetTaoRandomAd(2, AdType.TaoIndexAD);
    /// <summary>
    /// 3号广告
    /// </summary>
    protected string adlist3 = Advertisements.GetTaoRandomAd(3, AdType.TaoIndexAD);
    /// <summary>
    /// 4号广告
    /// </summary>
    protected string adlist4 = Advertisements.GetTaoRandomAd(4, AdType.TaoIndexAD);
    /// <summary>
    /// 5号广告
    /// </summary>
    protected string adlist5 = Advertisements.GetTaoRandomAd(5, AdType.TaoIndexAD);
    /// <summary>
    /// 6号广告
    /// </summary>
    protected AdShowInfo[] adlist6 = Advertisements.GetAdsByType(6, AdType.TaoIndexAD);
    /// <summary>
    /// 7号广告
    /// </summary>
    protected AdShowInfo[] adlist7 = Advertisements.GetAdsByType(7, AdType.TaoIndexAD);
    /// <summary>
    /// 8号广告
    /// </summary>
    protected AdShowInfo[] adlist8 = Advertisements.GetAdsByType(8, AdType.TaoIndexAD);
    /// <summary>
    /// 9号广告
    /// </summary>
    protected AdShowInfo[] adlist9 = Advertisements.GetAdsByType(9, AdType.TaoIndexAD);
    /// <summary>
    /// 10号广告
    /// </summary>
    protected AdShowInfo[] adlist10 = Advertisements.GetAdsByType(10, AdType.TaoIndexAD);
    /// <summary>
    /// 11号广告
    /// </summary>
    protected AdShowInfo[] adlist11 = Advertisements.GetAdsByType(11, AdType.TaoIndexAD);
    /// <summary>
    /// 12号广告
    /// </summary>
    protected string adlist12 = Advertisements.GetTaoRandomAd(12, AdType.TaoIndexAD);
    /// <summary>
    /// 13号广告
    /// </summary>
    protected AdShowInfo[] adlist13 = Advertisements.GetAdsByType(13, AdType.TaoIndexAD);

    protected override void ShowPage()
    {
        pagetitle = "淘之源-淘之购导购平台首页";
        seokeyword = "淘之源,商品导购";
        seodescription = "";
        ginfolist = TaoBaos.GetGoodsBrandList(Convert.ToInt16(TaoChanel.Index), 0);
        shoplist = TaoBaos.GetTaoBaoShopListByRecommend(Convert.ToInt16(TaoChanel.Index), 0);
        indextopiclist = TaoBaos.GetTaoBaoTopicList(Convert.ToInt16(TaoChanel.Index));
        taoactlist = Activities.GetTaoActivities();
        flinklist = SASLinks.GetFriendLinks();
    }
}
