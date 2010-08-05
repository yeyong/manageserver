using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Config;
using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Taobao;

public partial class activetyshow : TaoBaoPage
{
    protected ActivityInfo ainfo = new ActivityInfo();
    protected int aid = SASRequest.GetInt("aid", 0);

    protected override void ShowPage()
    {
        ainfo = Activities.GetActivityInfo(aid);

        if (ainfo == null)
        {
            AddErrLine("您的活动已过期或已删除！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }

        pagetitle = ainfo.Seotitle;
        seokeyword = ainfo.Seokeyword;
        seodescription = ainfo.Seodesc;
    }
}
