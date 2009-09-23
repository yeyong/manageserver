using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml;

using SAS.Common;
using SAS.Common.XML;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class managesubmenu : SAS.Web.UI.AdminPage
    {
        private string configPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            configPath = Utils.GetMapPath(BaseConfigs.GetSitePath.ToLower() + "ManagePage/xml/navmenu.config");
            string menuid = SASRequest.GetString("menuid");
            string submenuid = SASRequest.GetString("submenuid");
            string mode = SASRequest.GetString("mode");
            if (submenuid != "")
            {
                if (mode == "del")
                {
                    //DeleteSubItem(submenuid,menuid);
                    MenuManage.DeleteSubMenu(int.Parse(submenuid), int.Parse(menuid));
                }
                else
                {
                    if (submenuid == "0")
                    {
                        //NewSubMenu(menuid,SASRequest.GetString("menutitle"));
                        MenuManage.NewSubMenu(int.Parse(menuid), SASRequest.GetString("menutitle"));
                    }
                    else
                    {
                        //EditSubMenu(submenuid, SASRequest.GetString("menutitle"));
                        MenuManage.EditSubMenu(int.Parse(submenuid), SASRequest.GetString("menutitle"));
                    }
                }
                Response.Redirect("managesubmenu.aspx?menuid=" + menuid, true);
                return;
            }
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            string menuId = SASRequest.GetString("menuid");
            string menuTitle = "";
            string menuSubMenuList = "";
            XmlDocumentExtender doc = new XmlDocumentExtender();
            doc.Load(configPath);
            XmlNodeList mainMenus = doc.SelectNodes("/dataset/toptabmenu");
            foreach (XmlNode menuItem in mainMenus) //查找主菜单信息
            {
                if (menuItem["id"].InnerText == menuId)
                {
                    menuTitle = menuItem["title"].InnerText;
                    menuSubMenuList = menuItem["mainmenulist"].InnerText;
                    break;
                }
            }
            DataGrid1.TableHeaderName = menuTitle + "  菜单项管理";
            XmlNodeList subMenus = doc.SelectNodes("/dataset/mainmenu");
            DataTable dt = new DataTable();
            if (menuSubMenuList == "")  //该菜单没有子菜单
            {
                DataGrid1.DataSource = dt;
                DataGrid1.DataBind();
                return;
            }
            dt.Columns.Add("id");
            dt.Columns.Add("menuid");
            dt.Columns.Add("submenuid");
            dt.Columns.Add("menutitle");
            dt.Columns.Add("delitem");
            foreach (XmlNode subMenuItem in subMenus)
            {
                if (("," + menuSubMenuList + ",").IndexOf("," + subMenuItem["id"].InnerText + ",") != -1)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = subMenuItem["id"].InnerText;
                    dr["menuid"] = menuId;
                    dr["submenuid"] = subMenuItem["menuid"].InnerText;
                    dr["menutitle"] = subMenuItem["menutitle"].InnerText;
                    if (FindSubMenuItem(subMenuItem["menuid"].InnerText, doc))
                    {
                        dr["delitem"] = "删除";
                    }
                    else
                    {
                        dr["delitem"] = "<a href='managesubmenu.aspx?mode=del&menuid=" + menuId + "&submenuid=" + subMenuItem["id"].InnerText + "' onclick='return confirm(\"您确认要删除此菜单项吗？\");'>删除</a>";
                    }
                    dt.Rows.Add(dr);
                }
            }
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
        }

        private bool FindSubMenuItem(string menuid, XmlDocumentExtender doc)
        {
            XmlNodeList submenuitem = doc.SelectNodes("/dataset/submain");
            foreach (XmlNode item in submenuitem)
            {
                if (item["menuparentid"].InnerText == menuid)
                    return true;
            }
            return false;
        }
    }
}
