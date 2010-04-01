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
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-exchange.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/ScrollText.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.capSlide.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"ul:last\").hide();"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"p:first\").mouseover(function() {"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").show();"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").hide();"
                    + "\r\n " + "});"
                    + "\r\n " + "jQuery(\"#bulletin\").find(\"p:last\").mouseover(function() {"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:last\").show();"
                    + "\r\n " + "	jQuery(\"#bulletin\").find(\"ul:first\").hide();"
                    + "\r\n " + "});		"
                    + "\r\n " + "jQuery(\"#tao\").Exchange({ MIDS: \"onelt2tit\", CIDS: \"onelt2con\", count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#bill\").Exchange({ MIDS: \"onece1t\", CIDS: \"onece1con\", timer: 5000, count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#prod\").Exchange({ MIDS: \"onert3t\", CIDS: \"onert3con\", count: 5, mousetype: 1 });"
                    + "\r\n " + "jQuery(\"#mcd\").find(\"li\").capslide({ caption_color: 'black', caption_bgcolor: 'white', overlay_bgcolor: '#e7dad8', border: '2px solid #e7dad8', showcaption: true });"
                    + "\r\n " + "var scrollup = new ScrollText(\"listcomp\");"
                    + "\r\n " + "scrollup.LineHeight = 30;"
                    + "\r\n " + "scrollup.Amount = 2;"
                    + "\r\n " + "scrollup.Start();"
                    + "\r\n " + "});";
            AddfootScript(loadscript);
        }
    }
}
