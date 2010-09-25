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

public partial class amoy : TaoBaoPage
{
    protected override void ShowPage()
    {
        pagetitle = "魅惑初秋 － 淘之购";
        seokeyword = "秋装，单品，韩版，英伦，西装，马夹，针织衫，单鞋，皮鞋";
        seodescription = "2010淘之购推荐秋季单品，特色品牌推荐，秋之惑。";
    }
}
