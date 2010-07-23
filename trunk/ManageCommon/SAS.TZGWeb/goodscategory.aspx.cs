using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.TaoBao;
using SAS.Entity;
using SAS.Taobao;
using SAS.Taobao.Request;

public partial class goodscategory : TaoBaoPage
{
    protected List<CategoryInfo> parentcinfo = TaoBaos.GetCategoryListByParentID(0);

    protected override void ShowPage()
    {
        
    }
}
