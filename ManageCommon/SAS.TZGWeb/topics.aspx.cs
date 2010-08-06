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

public partial class topics : TaoBaoPage
{
    protected List<TaoBaoTopicInfo> tbtlist = new List<TaoBaoTopicInfo>();

    protected override void ShowPage()
    {
        tbtlist = TaoBaos.GetTaoBaoTopicList();

        pagetitle = "专题展-淘之购专题展示";
        seokeyword = "专题展,淘之购专题,淘宝商品专题,商品专题展,浙商黄页专题";
        seodescription = "淘之购专题展示，全面展示特色商品，各个商品展区。";
    }
}
