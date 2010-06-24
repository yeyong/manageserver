using System;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class sitemap : CompanyPage
    {
        protected DataRow[] mapcataloglist = Catalogs.GetAllCatalogBySort(1);
        /// <summary>
        /// 导航
        /// </summary>
        protected DataRow[] mapnavlist1 = Navs.GetNavigationByPid(0);
        protected DataRow[] mapnavlist2 = Navs.GetNavigationByPid(4);

        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
        }
    }
}
