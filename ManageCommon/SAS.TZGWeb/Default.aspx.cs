using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.TaoBao;
using SAS.Entity.Domain;
using SAS.Taobao;

public partial class _Default : TaoBaoPage
{
    protected long itemcount = 0;
    protected List<ItemCat> taoitemcatlist = TaoBaos.GetItemCatCache();

    protected override void ShowPage()
    {
        pagetitle = "淘之够首页";
        itemcount = TaoBaos.GetItemCatCache().Count;
    }
}
