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

public partial class woman : TaoBaoPage
{
    protected List<RecommendWithProduct> rwplist = new List<RecommendWithProduct>();

    protected override void ShowPage()
    {
        rwplist = TaoBaos.GetProductWithRecommend(Convert.ToInt16(TaoChanel.Woman));
    }
}
