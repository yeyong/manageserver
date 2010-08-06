using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.TaoBao;
using SAS.Entity;
using SAS.Taobao;
using SAS.Taobao.Request;

public partial class goodscategory : TaoBaoPage
{
    protected List<CategoryInfo> parentcinfo = TaoBaos.GetCategoryListByParentID(0);

    protected override void ShowPage()
    {
        pagetitle = "商品类目大全-商品类目导购";
        seokeyword = "淘宝商品类目,淘之购商品类目,浙商黄页商品类目";
        seodescription = "商品类目大全，淘之购收集整理并推荐的淘之购商品类别大全。";
    }
}
