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
            Companys companyinfo = Companies.GetCompanyCacheInfo(enid);

            if (companyinfo == null)
            {
                AddErrLine("页面出现错误");
                SetMetaRefresh(3);
                return;
            }

            if (ispost)
            {
                string builddate = SASRequest.GetString("regdate");
                int postentype = SASRequest.GetInt("postentype", 1);
                int postcommtype = SASRequest.GetInt("postcommtype", 1);
                int regcapital = SASRequest.GetInt("regcapital", 0);
                string regsign = SASRequest.GetString("regsign");
                string regorgan = SASRequest.GetString("regorgan");
                string lastdate = SASRequest.GetString("lastdate");
                string limitdate = SASRequest.GetString("limitdate");
                string regaddress = Utils.RemoveHtml(SASRequest.GetString("regaddress"));
                string regmain = SASRequest.GetString("regmain");

                companyinfo.En_builddate = builddate;
                companyinfo.En_type = postentype;
                companyinfo.En_enco = postcommtype;
                companyinfo.Reg_capital = regcapital.ToString();
                companyinfo.Reg_code = regsign;
                companyinfo.Reg_organ = regorgan;
                companyinfo.Reg_year = lastdate;
                companyinfo.Reg_date = limitdate;
                companyinfo.Reg_address = regaddress;
                companyinfo.En_main = regmain;

                companyinfo.En_status = 1;
                companyinfo.En_reason = "";

                if (!Companies.UpdateCompanyInfo(companyinfo))
                {
                    AddErrLine("页面出现错误，请与管理员联系！");
                    SetMetaRefresh(3);
                    return;
                }
                else
                {
                    AddMsgLine("提交成功，请您耐心等待管理员的信息审核！");
                    SetMetaRefresh(3, "index.html");
                    return;
                }
            }
        }
    }
}
