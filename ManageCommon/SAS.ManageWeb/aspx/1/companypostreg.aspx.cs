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
        protected override void ShowPage()
        {
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "var theprifix = \"v2_\";"
                    + "\r\n " + "jQuery(\"#form1\").FormValidFunc(theprifix);"
                    + "\r\n " + "jQuery(\"input[type=text],textarea\").each("
                    + "\r\n " + "  function(){jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});"
                    + "\r\n " + "});"
                    + "\r\n " + "});";
            AddfootScript(loadscript);
        }
    }
}
