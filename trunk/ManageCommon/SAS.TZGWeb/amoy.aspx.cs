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
        pagetitle = "淘清凉-夏日必备单品";
        seokeyword = "淘之购泡沫之夏,泡沫之夏,夏日单品,妆容,美妆,配饰,美装";
        seodescription = "泡沫之夏，炎炎夏日提供淘宝清凉单品推荐。";
    }
}
