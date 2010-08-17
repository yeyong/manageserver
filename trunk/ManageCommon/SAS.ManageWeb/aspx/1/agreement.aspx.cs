using System;
using System.Text;
using System.Data;
using System.Web;

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
                    HttpContext.Current.Response.Redirect("companypost.aspx");
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
