﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Logic;
using SAS.Taobao;

public partial class index : TaoBaoPage
{
    protected override void ShowPage()
    {
        pagetitle = "商之源";
    }
}
