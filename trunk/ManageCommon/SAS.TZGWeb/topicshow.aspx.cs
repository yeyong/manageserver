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

public partial class topicshow : TaoBaoPage
{
    protected TaoBaoTopicInfo tinfo = new TaoBaoTopicInfo();
    protected long tid = long.Parse(SASRequest.GetString("tid"));

    protected override void ShowPage()
    {
        tinfo = TaoBaos.GetTaoBaoTopicInfo(tid);
        if (tinfo == null) AddErrLine("您的专题不存在或已删除！");

        if (page_err > 0) return;

        pagetitle = tinfo.Title;
    }
}
