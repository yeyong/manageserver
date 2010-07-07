using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class agreement : CompanyPage
    {
        protected int agreestatus = SASRequest.GetInt("submitstatus", 0);
        protected override void ShowPage()
        {
            if (ispost)
            {
                if (agreestatus == 1)
                {
                    AddMsgLine("感谢您的理解和支持！页面3秒钟后自动转到提交页面。");
                    SetMetaRefresh(3, rooturl + "companypost.aspx");
                }
                else
                {
                    AddMsgLine("感谢您的参与，期待与您下一次的合作！页面3秒钟后自动转到上一个页面。");
                    SetMetaRefresh(3, rooturl + LogicUtils.GetReUrl());
                }
            }
        }
    }
}
