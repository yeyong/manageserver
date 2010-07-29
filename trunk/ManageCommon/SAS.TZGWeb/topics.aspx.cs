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
    }
}
