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

public partial class mastertopic : System.Web.UI.MasterPage
{
    /// <summary>
    /// 主站链接
    /// </summary>
    protected string mainsiteurl = GeneralConfigs.GetConfig().Weburl;
    /// <summary>
    /// 淘之购子菜单
    /// </summary>
    protected DataRow[] subnavs = Navs.GetNavigationByPid(4);
    /// <summary>
    /// 当前页面名称
    /// </summary>
    protected string currentpagename = "";
    protected GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();
    /// <summary>
    /// 站点根目录
    /// </summary>
    protected string therooturl = Utils.GetRootUrl(BaseConfigs.GetSitePath);
    protected List<HelpInfo> helplist = Helps.GetTaoIndexHelp();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
