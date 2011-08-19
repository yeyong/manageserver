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
            AddLinkCss(rooturl + "images/validatorAuto.css");
            script += "\r\n<script src=\"" + rooturl + "javascript/companycategories.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/locations.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/template_catalogadmin.js\" type=\"text/javascript\"></script>";
            string loadscript = "\r\n " + "jQuery(document).ready(function() {";
            loadscript += "\r\n " + "jQuery(\"#moveup\").click(function() { jQuery(this).CatalogMoveUp(\"zyhy\", \"selecthy\"); });";
            loadscript += "\r\n " + "jQuery(\"#movedown\").click(function() { jQuery(this).CatalogMoveDown(\"selecthy\"); });";
            loadscript += "\r\n " + "initCategory(\"zyhy\");";
            loadscript += "\r\n " + "jQuery(\"#thelocation\").InitLocation();";
            loadscript += "\r\n " + "var theprifix = \"v2_\";";
            loadscript += "\r\n " + "jQuery(\"#form1\").FormValidFunc(theprifix);";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery(\"input[type=text],textarea\").each(";
                loadscript += "\r\n " + "  function(){jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "});";
            loadscript += "\r\n " + "function validate(theform){";
            loadscript += "\r\n " + "  if (form1.selecthy.options.length == 0) {";
            loadscript += "\r\n " + "    document.getElementById(\"nextsub\").disabled = false;";
            loadscript += "\r\n " + "    alert(\"请选择公司主营行业类别！\");";
            loadscript += "\r\n " + "    return false;";
            loadscript += "\r\n " + "  }";
            loadscript += "\r\n " + "  if (document.getElementById(\"district\").value == \"\" || document.getElementById(\"district\").value == \"0\") {";
            loadscript += "\r\n " + "    document.getElementById(\"nextsub\").disabled = false;";
            loadscript += "\r\n " + "    alert(\"请准确选择公司所在地区！\");";
            loadscript += "\r\n " + "    return false;";
            loadscript += "\r\n " + "  }";
            loadscript += "\r\n " + "  var hylist = \"\";";
            loadscript += "\r\n " + "  for (var i = 0; i < form1.selecthy.options.length; i++) {";
            loadscript += "\r\n " + "    if (i == 0) {";
            loadscript += "\r\n " + "      hylist = form1.selecthy.options[i].value;";
            loadscript += "\r\n " + "    } else {";
            loadscript += "\r\n " + "      hylist += \",\" + form1.selecthy.options[i].value;";
            loadscript += "\r\n " + "    }";
            loadscript += "\r\n " + "  }";
            loadscript += "\r\n " + "  document.getElementById(\"hyidlist\").value = hylist;";
            loadscript += "\r\n " + "  return true;";
            loadscript += "\r\n " + "}";
            AddfootScript(loadscript);

            if (ispost)
            {
                string qyname = SASRequest.GetString("qyname");                 //企业名称
                string builddate = SASRequest.GetString("builddate");           //企业成立时间
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

                Companys companyinfo = new Companys();
                companyinfo.En_name = qyname;
                companyinfo.En_builddate = builddate;
                companyinfo.En_cataloglist = hycata;
                companyinfo.En_corp = corper;
                companyinfo.En_contact = contor;
                companyinfo.En_phone = phone;
                companyinfo.En_mobile = mobile;
                companyinfo.En_fax = fax;
                companyinfo.En_mail = email;
                companyinfo.En_web = enweb;
                companyinfo.En_areas = district;
                companyinfo.En_address = address;
                companyinfo.En_post = zipcode;
                companyinfo.En_desc = desc;
                companyinfo.En_visble = 0;
                companyinfo.En_status = 0;

                companyinfo.En_reason = "信息尚未完成，请完成企业注册信息后才能进入申请状态！";

                int companyid = Companies.CreateCompanyInfo(companyinfo);

                SetUrl("companypostreg.aspx?companyid=" + companyid);
                SetMetaRefresh(0);
            }
        }
    }
}
