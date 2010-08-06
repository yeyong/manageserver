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

public partial class brand : TaoBaoPage
{
    protected List<CategoryInfo> classlist = TaoBaos.GetCategoryListByParentID(0);

    protected override void ShowPage()
    {
        pagetitle = "品牌馆-品牌商品导购";
        seokeyword = "畅销品牌,淘宝品牌,网上品牌,淘之购品牌,浙商黄页品牌";
        seodescription = "淘宝品牌导购，淘之购为您推荐的热门品牌导购。";
    }
}
