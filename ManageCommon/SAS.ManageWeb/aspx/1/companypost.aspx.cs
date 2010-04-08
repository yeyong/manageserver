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
                    + "\r\n " + "});"
                    + "\r\n " + "function validate(theform){"
                    + "\r\n " + "  if (form1.selecthy.options.length == 0) {"
                    + "\r\n " + "    document.getElementById(\"nextsub\").disabled = false;"
                    + "\r\n " + "    alert(\"请选择公司主营行业类别！\");"
                    + "\r\n " + "    return false;"
                    + "\r\n " + "  }"
                    + "\r\n " + "  if (document.getElementById(\"district\").value == \"\" || document.getElementById(\"district\").value == \"0\") {"
                    + "\r\n " + "    document.getElementById(\"nextsub\").disabled = false;"
                    + "\r\n " + "    alert(\"请准确选择公司所在地区！\");"
                    + "\r\n " + "    return false;"
                    + "\r\n " + "  }"
                    + "\r\n " + "  var hylist = \"\";"
                    + "\r\n " + "  for (var i = 0; i < form1.selecthy.options.length; i++) {"
                    + "\r\n " + "    if (i == 0) {"
                    + "\r\n " + "      hylist = form1.selecthy.options[i].value;"
                    + "\r\n " + "    } else {"
                    + "\r\n " + "      hylist += \",\" + form1.selecthy.options[i].value;"
                    + "\r\n " + "    }"
                    + "\r\n " + "  }"
                    + "\r\n " + "  document.getElementById(\"hyidlist\").value = hylist;"
                    + "\r\n " + "  return true;"
                    + "\r\n " + "}";
            AddfootScript(loadscript);

            if (ispost)
            {
                string qyname = SASRequest.GetString("qyname");                 //企业名称
                string hycata = SASRequest.GetString("hyidlist");               //行业类别
                string corper = SASRequest.GetString("corpname");               //企业法人
                string contor = SASRequest.GetString("contactor");              //主要联系人
                string phone = SASRequest.GetString("phone");                   //固定电话
                string mobile = SASRequest.GetString("mobile");                 //手机
                string fax = SASRequest.GetString("fax");                       //传真
                string email = SASRequest.GetString("email");                   //电子邮箱
                string enweb = SASRequest.GetString("enweb");                   //企业网址
                int district = SASRequest.GetInt("district", 0);                //地区
                string address = SASRequest.GetString("address");               //地址
                string zipcode = SASRequest.GetString("zipcode");               //邮编
                string desc = Utils.HtmlEncode(SASRequest.GetString("desc"));   //企业描述
            }
        }
    }
}
