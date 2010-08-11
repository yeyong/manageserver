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

public partial class chanelinfo : TaoBaoPage
{
    /// <summary>
    /// 频道ID
    /// </summary>
    protected int chanelid = SASRequest.GetInt("cid", 0);
    /// <summary>
    /// 频道信息
    /// </summary>
    protected CategoryInfo crootinfo = new CategoryInfo();
    /// <summary>
    /// 频道品牌列表
    /// </summary>
    protected List<GoodsBrandInfo> cbrandlist = new List<GoodsBrandInfo>();
    protected List<CategoryInfo> csubclasslist = new List<CategoryInfo>();
    protected List<RecommendWithProduct> rproductlist = new List<RecommendWithProduct>();
    /// <summary>
    /// 1号广告
    /// </summary>
    protected string adlist1 = "";
    /// <summary>
    /// 2号广告
    /// </summary>
    protected string adlist2 = "";
    /// <summary>
    /// 3号广告
    /// </summary>
    protected string adlist3 = "";
    /// <summary>
    /// 4号广告
    /// </summary>
    protected string adlist4 = "";
    /// <summary>
    /// 5号广告
    /// </summary>
    protected string adlist5 = "";

    protected override void ShowPage()
    {
        crootinfo = TaoBaos.GetChanelInfoByCache(chanelid);

        if (crootinfo == null)
        {
            AddErrLine("频道信息出错！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }

        cbrandlist = TaoBaos.GetGoodsBrandListByClass(chanelid);
        csubclasslist = TaoBaos.GetCategoryListByParentID(chanelid);
        //rproductlist = 

        adlist1 = Advertisements.GetTaoRandomAd(chanelid * 10 + 1, AdType.TaoChanelAd);
        adlist2 = Advertisements.GetTaoRandomAd(chanelid * 10 + 2, AdType.TaoChanelAd);
        adlist3 = Advertisements.GetTaoRandomAd(chanelid * 10 + 3, AdType.TaoChanelAd);
        adlist4 = Advertisements.GetTaoRandomAd(chanelid * 10 + 4, AdType.TaoChanelAd);
        adlist5 = Advertisements.GetTaoRandomAd(chanelid * 10 + 5, AdType.TaoChanelAd);

        pagetitle = crootinfo.Name + "频道-" + crootinfo.Name + "类型商品导购";
        seokeyword = string.Format("{0}频道,{0}类别,{0}商品,{0}导购,{0}类目,{0}搜索", crootinfo.Name);
        seodescription = string.Format("{0}频道，淘之购提供专业的{0}淘宝商品导购与商品推荐。", crootinfo.Name);
    }
}
