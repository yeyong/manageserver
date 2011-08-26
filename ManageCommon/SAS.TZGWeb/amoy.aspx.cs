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
        pagetitle = "魅惑初秋 床上用品四件套 － 淘之购";
        seokeyword = "四件套，床上用品，今秋特惠，特价区";
        seodescription = "床上用品四件套，淘之购魅惑初秋特价区，田园系列四件套，婚庆系列四件套，缤纷系列四件套。";
    }
}
