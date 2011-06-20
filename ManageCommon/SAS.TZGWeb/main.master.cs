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

public partial class main : System.Web.UI.MasterPage
{
    /// <summary>
    /// 类别集合
    /// </summary>
    protected List<CategoryInfo> parentcategorylist = new List<CategoryInfo>();
    /// <summary>
    /// 站点根目录
    /// </summary>
    protected string therooturl = Utils.GetRootUrl(BaseConfigs.GetSitePath);
    /// <summary>
    /// 主站链接
    /// </summary>
    protected string mainsiteurl = GeneralConfigs.GetConfig().Weburl;
    /// <summary>
    /// 淘之购子菜单
    /// </summary>
    protected DataRow[] subnavs = Navs.GetNavigationByPid(4);
    /// <summary>
    /// 头部广告
    /// </summary>
    protected string taoindexheadad = Advertisements.GetTaoHeaderAd();
    /// <summary>
    /// 淘头部广告
    /// </summary>
    protected AdShowInfo[] ainfolist = Advertisements.GetAdsByType(1, AdType.TaoIndexHeaderAD);
    /// <summary>
    /// 当前页面名称
    /// </summary>
    protected string currentpagename = "";
    protected GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();
    protected List<HelpInfo> helplist = Helps.GetCommonHelp();

    protected void Page_Load(object sender, EventArgs e)
    {
        currentpagename = SASRequest.GetPageName().Split('.')[0];
        parentcategorylist = TaoBaos.GetCategoryListByParentID(0);
    }
}
