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
    public class companypost : CompanyPage
    {
        protected override void ShowPage()
        {
            script += "\r\n<script src=\"" + forumpath + "javascript/companycategories.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/locations.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/template_catalogadmin.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#moveup\").click(function() { jQuery(this).CatalogMoveUp(\"zyhy\", \"selecthy\"); });"
                    + "\r\n " + "jQuery(\"#movedown\").click(function() { jQuery(this).CatalogMoveDown(\"selecthy\"); });"
                    + "\r\n " + "initCategory(\"zyhy\");"
                    + "\r\n " + "jQuery(\"#thelocation\").InitLocation();"
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
