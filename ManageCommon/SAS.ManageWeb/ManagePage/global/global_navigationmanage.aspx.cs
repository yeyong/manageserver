using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

using SAS.Common;
using SAS.Config;
using SAS.Control;
using SAS.Logic;
using SAS.Entity;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_navigationmanage : AdminPage
    {
        private DataTable navMenuTable = Navs.GetNavigation(true);
        protected void Page_Load(object sender, EventArgs e)
        {
            DataGrid1.DataKeyField = "id";
            string menuid = SASRequest.GetString("menuid");
            string mode = SASRequest.GetString("mode");
            if (mode != "")
            {
                if (mode == "del")
                {
                    Navs.DeleteNavigation(SASRequest.GetQueryInt("id", 0));
                    Response.Redirect(Request.Path + (SASRequest.GetString("parentid") != "" ? "?parentid=" + SASRequest.GetString("parentid") : ""), true);
                }
                else
                {
                    if (SASRequest.GetFormString("name").Trim() == "" || SASRequest.GetFormString("displayorder").Trim() == "" || SASRequest.GetFormInt("displayorder", 0) > Int16.MaxValue)
                    {
                        this.RegisterStartupScript("", "<script type='text/javascript'>alert('名称或序号输入不合法。');window.location=window.location;</script>");
                        return;
                    }
                    if (menuid == "0")
                    {
                        NavInfo nav = new NavInfo();
                        nav.Parentid = SASRequest.GetQueryInt("parentid", 0);
                        GetFromData(nav);
                        Navs.InsertNavigation(nav);

                    }
                    else
                    {
                        NavInfo nav = new NavInfo();
                        nav.Id = SASRequest.GetFormInt("menuid", 0);
                        GetFromData(nav);
                        Navs.UpdateNavigation(nav);
                    }
                    Response.Redirect(Request.RawUrl, true);
                }
            }
            else
            {
                BindDataGrid(SASRequest.GetQueryInt("parentid", 0));
                if (SASRequest.GetString("parentid") == "")
                {
                    returnbutton.Visible = false;
                }
            }
        }

        private void GetFromData(NavInfo nav)
        {
            nav.Name = GetMaxlengthString(SASRequest.GetFormString("name"), 50);
            nav.Title = GetMaxlengthString(SASRequest.GetFormString("title"), 255);
            nav.Url = GetMaxlengthString(SASRequest.GetFormString("url"), 255);
            nav.Target = SASRequest.GetFormInt("target", 0);
            nav.Available = SASRequest.GetFormInt("available", 0);
            nav.Displayorder = SASRequest.GetFormInt("displayorder", 0);
            nav.Level = SASRequest.GetFormInt("level", 0);
        }

        private string GetMaxlengthString(string str, int len)
        {
            return str.Length <= len ? str : str.Substring(0, len);
        }

        private void BindDataGrid(int parentid)
        {
            DataGrid1.TableHeaderName = (parentid != 0 ? "子" : "") + "导航菜单管理";
            DataGrid1.AllowCustomPaging = false;

            DataTable navmenu = navMenuTable.Clone();
            foreach (DataRow dr in navMenuTable.Select("parentid=" + parentid))
            {
                navmenu.ImportRow(dr);
            }
            DataGrid1.DataSource = navmenu;
            DataGrid1.DataBind();
            string navscript = "\r\n<script type='text/javascript'>\r\nnav = [";
            foreach (DataRow dr in navmenu.Rows)
            {
                navscript += String.Format("\r\n{{id:'{0}',parentid:'{1}',name:'{2}',title:'{3}',url:'{4}',target:'{5}',type:'{6}',available:'{7}',displayorder:'{8}',level:'{9}'}},",
                    dr["id"], dr["parentid"], dr["name"], dr["title"], dr["url"], dr["target"], dr["navstype"], dr["available"], dr["displayorder"], dr["level"]);
            }
            navscript = navscript.TrimEnd(',') + "]\r\n</script>";
            this.RegisterStartupScript("", navscript);
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region 数据绑定显示长度控制

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox t = (TextBox)e.Item.Cells[0].Controls[0];
                t.Attributes.Add("size", "4");

                t = (TextBox)e.Item.Cells[2].Controls[0];
                t.Attributes.Add("size", "20");
            }

            #endregion
        }

        protected void saveNav_Click(object sender, EventArgs e)
        {
            int row = 0;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                int id = int.Parse(o.ToString());
                string displayorder = DataGrid1.GetControlValue(row, "displayorder").Trim();
                string url = DataGrid1.GetControlValue(row, "url").Trim();
                NavInfo nav = Navs.GetNavigation(id);
                if (nav == null)
                    continue;
                if (!Utils.IsNumeric(displayorder) || url == "")
                {
                    row++;
                    continue;
                }
                if (nav.Displayorder != int.Parse(displayorder) || nav.Url != url)
                {
                    nav.Displayorder = int.Parse(displayorder);
                    nav.Url = url;
                    Navs.UpdateNavigation(nav);
                }
                row++;
            }
            Response.Redirect(Request.RawUrl, true);
        }

        protected string GetSubNavMenuManage(string id, string type)
        {
            if ((navMenuTable.Select("parentid=" + id).Length != 0 || type == "1") && SASRequest.GetString("parentid") == "")
            {
                return String.Format("<a href=\"?parentid={0}\">管理子菜单</a>", id);
            }
            return "";
        }

        protected string GetDeleteLink(string id, string type)
        {
            if (type == "1" && navMenuTable.Select("parentid=" + id).Length == 0)
            {
                return String.Format("<a href=\"?{0}mode=del&id={1}\" onclick=\"return confirm('确认要将该菜单项删除吗?');\">删除</a>",
                    (SASRequest.GetString("parentid") != "" ? "parentid=" + SASRequest.GetString("parentid") + "&" : ""), id);
            }
            return "";
        }

        protected string GetLink(string url)
        {
            if (url.ToLower().StartsWith("http://"))
                return url;
            return String.Format("../../{0}", url);
        }

        protected string GetLevel(string level)
        {
            switch (level)
            {
                case "0":
                    return "游客";
                case "1":
                    return "会员";
                case "2":
                    return "版主";
                case "3":
                    return "管理员";
            }
            return "";
        }
    }
}
