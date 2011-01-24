using System;
using System.IO;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class zswall : CompanyPage
    {
        protected List<Companys> wallcompany = Companies.GetCompanyListWall();

        protected override void ShowPage()
        {
            string m_keyword = "浙商黄页,名片夹,名片墙";  //meta关键字
            string m_content = "浙商黄页(www.cnzshy.com)浙商企业信息检索。大力扶持中小企业，中小型企业的摇篮，免费的企业展示平台、企业推广平台，让所有的网站都成为您企业的展示平台，更多服务尽在浙商黄页展示平台！";  //meta内容描述
            pagetitle = "浙商黄页|浙商企业名片夹";
            UpdateMetaInfo(m_keyword, m_content, "");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(forumpath + "templates/" + templatepath + "/css/card.css");
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery-ui.min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.transform-0.6.2.min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/jquery.animate-shadow-min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + forumpath + "javascript/zswall.js\" type=\"text/javascript\"></script>";
        }
    }
}
