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
    public class companypostreg : CompanyPage
    {
        protected string testdate = Utils.GetTime();
        protected int enid = 0;
        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "images/validator.css");
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "var theprifix = \"v2_\";"
                    + "\r\n " + "jQuery(\"#form1\").FormValidFunc(theprifix,1,1);"
                    + "\r\n " + "jQuery(\"input[type=text],textarea\").each("
                    + "\r\n " + "  function(){jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
                    + "\r\n " + "});"
                    + "\r\n " + "});";
            AddfootScript(loadscript);
            enid = SASRequest.GetInt("companyid", 0);
            if (enid == 0)
            {
                AddErrLine("页面出现错误");
                return;
            }
        }
    }
}
