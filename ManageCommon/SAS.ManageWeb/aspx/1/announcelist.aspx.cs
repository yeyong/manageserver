using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class announcelist : CompanyPage
    {
        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
        }
    }
}
