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

    protected override void ShowPage()
    {

    }
}
