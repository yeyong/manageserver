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
    public class zspage : CompanyPage
    {
        protected override void ShowPage()
        {
            pagetitle = "浙商黄页-黄页频道";
            UpdateMetaInfo("浙商,浙商黄页,黄页频道,杭州企业,企业推广", "黄页频道-浙商黄页的企业推荐平台，推荐包括工业、商业服务、公共服务及社会组织等四类标准行业的浙江企业。", "");
        }
    }
}
