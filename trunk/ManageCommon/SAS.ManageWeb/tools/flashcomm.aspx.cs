using System;
using System.Web;
using System.Web.UI;
using System.Text;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;

namespace SAS.ManageWeb.tools
{
    public partial class flashcomm : System.Web.UI.Page
    {
        private int objid = SASRequest.GetInt("objid", 0);

        protected void Page_Load(object sender, EventArgs e)
        {
            Companys companyinfo = Companies.GetCompanyCacheInfo(objid);
            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");

            if (companyinfo == null)
            {
                xmlnode.Append("<data><error>企业信息读取错误！请您与管理员联系！" + objid + "</error></data>");
                ResponseXML(xmlnode);
                return;
            }

            xmlnode.Append("<data>");
            xmlnode.AppendFormat("<ctitle>{0}</ctitle>", companyinfo.En_name);
            xmlnode.AppendFormat("<relate>{0}</relate>", companyinfo.En_contact);
            xmlnode.AppendFormat("<phone>{0}</phone>", companyinfo.En_phone);
            xmlnode.AppendFormat("<address>{0}</address>", companyinfo.En_address);
            xmlnode.AppendFormat("<website>{0}</website>", companyinfo.En_web);
            xmlnode.Append("</data>");
            ResponseXML(xmlnode);
        }

        private void ResponseXML(StringBuilder xmlnode)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
