using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Config;
using SAS.Control;
using SAS.Logic;
using SAS.Common.XML;

namespace SAS.ManageWeb.ManagePage
{
    public partial class managesubmenuitem : AdminPage
    {
        private string configPath;
        public string menuid;
        public string submenuid;
        public string pagename;
        protected void Page_Load(object sender, EventArgs e)
        {
            configPath = Utils.GetMapPath(BaseConfigs.GetSitePath.ToLower() + "ManagePage/xml/navmenu.config");
            menuid = SASRequest.GetString("menuid");
            submenuid = SASRequest.GetString("submenuid");
            pagename = SASRequest.GetString("pagename");
            string id = SASRequest.GetString("id");
            string mode = SASRequest.GetString("mode");
            if (id != "")
            {
                if (mode == "del")
                {
                    //DeleteMainItem(id);
                    MenuManage.DeleteMenuItem(int.Parse(id));
                }
                else
                {
                    if (id == "-1")
                    {
                        MenuManage.NewMenuItem(int.Parse(submenuid), SASRequest.GetString("menutitle"), SASRequest.GetString("link"));
                    }
                    else
                    {
                        MenuManage.EditMenuItem(int.Parse(id), SASRequest.GetString("menutitle"), SASRequest.GetString("link"));
                    }
                }
                Response.Redirect("managesubmenuitem.aspx?menuid=" + menuid + "&submenuid=" + submenuid + "&pagename=" + SASRequest.GetString("pagename"), true);
            }
            else
            {
                if (!IsPostBack)
                {
                    BindDataGrid();
                }
            }
        }

        private void BindDataGrid()
        {
            DataGrid1.TableHeaderName = pagename + " 子菜单项管理";
            XmlDocumentExtender doc = new XmlDocumentExtender();
            doc.Load(configPath);
            string submenuid = SASRequest.GetString("submenuid");
            XmlNodeList submenusitem = doc.SelectNodes("/dataset/submain");
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id"));
            dt.Columns.Add(new DataColumn("menutitle"));
            dt.Columns.Add(new DataColumn("link"));
            int i = 0;
            foreach (XmlNode menuitem in submenusitem)
            {
                if (menuitem["menuparentid"].InnerText == submenuid)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = i.ToString();
                    dr["menutitle"] = menuitem["menutitle"].InnerText;
                    dr["link"] = menuitem["link"].InnerText;
                    dt.Rows.Add(dr);
                }
                i++;
            }
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
        }
    }
}
