using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb
{
    public class sitemap : CompanyPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected TaoBaoConfigInfo tbconfig = TaoBaoConfigs.GetConfig();
        protected DataRow[] mapcataloglist = Catalogs.GetAllCatalogBySort(1);
        /// <summary>
        /// 导航
        /// </summary>
        protected DataRow[] mapnavlist1 = Navs.GetNavigationByPid(0);
        protected DataRow[] mapnavlist2 = Navs.GetNavigationByPid(4);
        protected List<CategoryInfo> categorylist = new List<CategoryInfo>();

        protected string[] newsclass = { "060057025098", "250846114252", "382032510445", "167681906525", "105376361575", "619128150758", "871599573494", "144785272071" };

        protected override void ShowPage()
        {
            pagetitle = "站点地图-浙商站点地图";
            UpdateMetaInfo("站点地图", "浙商站点地图，展示浙商主站及分站全貌。", "");
            AddLinkCss(rooturl + "templates/" + templatepath + "/css/channels.css");
            categorylist = tpb.GetCategoryListByParentID(0);
        }

        protected List<CategoryInfo> GetCategoryList(int cid)
        {
            return tpb.GetCategoryListByParentID(cid);
        }
    }
}
