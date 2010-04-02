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
                    + "\r\n " + "jQuery(\"#moveup\").click(function() { $(this).CatalogMoveUp(\"zyhy\", \"selecthy\"); });"
                    + "\r\n " + "jQuery(\"#movedown\").click(function() { $(this).CatalogMoveDown(\"selecthy\"); });"
                    + "\r\n " + "initCategory(\"zyhy\");"
                    + "\r\n " + "jQuery(\"#thelocation\").InitLocation();"
                    + "\r\n " + "});";
            AddfootScript(loadscript);
        }
    }
}
