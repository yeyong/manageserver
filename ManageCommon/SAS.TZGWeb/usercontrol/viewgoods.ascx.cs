using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Common;

public partial class usercontrol_viewgoods : System.Web.UI.UserControl
{
    protected string viewlist = "";

    public usercontrol_viewgoods()
    {
        viewlist = Utils.GetCookie("goodviews").Trim(',');
        if (viewlist == "") return;
    }
}
