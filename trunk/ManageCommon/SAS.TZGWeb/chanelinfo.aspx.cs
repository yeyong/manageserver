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
    }
}
