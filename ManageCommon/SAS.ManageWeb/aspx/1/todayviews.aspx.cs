using System;
using System.IO;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class todayviews : CompanyPage
    {
        /// <summary>
        /// ID集合
        /// </summary>
        protected string[] todaycompanyid;

        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            todaycompanyid = Utils.GetCookie("lastviews").Trim(',').Split(',');
        }
    }
}
