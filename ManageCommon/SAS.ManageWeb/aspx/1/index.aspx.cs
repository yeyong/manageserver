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
    public class index : CompanyPage
    {
        protected override void ShowPage()
        {
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/main.css");
            script += "\r\n<script src=\"" + forumpath +"javascript/jquery.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-exchange.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\njQuery(document).ready(function() {"
                    + "\r\njQuery(\"#bill\").Exchange({ MIDS: \"onece2tit\", CIDS: \"onece2con\", timer: 5000, count: 5, mousetype: 1 });"
                    + "\r\njQuery(\"#find\").Exchange({ MIDS: \"onece4tit\", CIDS: \"onece4con\", timer: 0, count: 5 });"
                    + "\r\n});";
            AddScript(loadscript);
        }
    }
}
