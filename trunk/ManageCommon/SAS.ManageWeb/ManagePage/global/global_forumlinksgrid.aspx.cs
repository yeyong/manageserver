using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using Button = SAS.Control.Button;
using DataGrid = SAS.Control.DataGrid;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 友情链接操作
    /// </summary>
    public partial class global_forumlinksgrid : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }

            #region 插入相关友情链接记录

            if ((SASRequest.GetString("displayorder").Trim() != "") && (SASRequest.GetString("name").Trim() != ""))
            {
                Regex r = new Regex("(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w-./?%&=]*)?");
                if (!r.IsMatch(SASRequest.GetString("url").Replace("'", "''")))
                {
                    base.RegisterStartupScript("", "<script>alert('链接地址或LOGO不是有效的网页地址.');</script>");
                    return;
                }

                try
                {
                    SASLinks.CreateSASLink(SASRequest.GetInt("displayorder", 0), SASRequest.GetString("name"), SASRequest.GetString("url"), SASRequest.GetString("note"),
                        SASRequest.GetString("logo"));
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "添加友情链接", "添加友情链接,名称为: " + SASRequest.GetString("name"));
                    BindData();
                }
                catch
                {
                    base.RegisterStartupScript("", "<script>alert('无法更新数据库');window.location.href='global_forumlinksgrid.aspx';</script>");
                    return;
                }
            }

            #endregion
        }

        public void BindData()
        {
            #region 绑定友情链接列表
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "友情链接列表";
            DataGrid1.BindData(SASLinks.GetSASLinks());
            #endregion
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        protected void SaveFriend_Click(Object sender, EventArgs e)
        {
            #region 保存友情链接修改
            int row = 0;
            bool error = false;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                int displayorder = int.Parse(DataGrid1.GetControlValue(row, "displayorder"));
                string name = DataGrid1.GetControlValue(row, "name").Trim();
                string url = DataGrid1.GetControlValue(row, "linkurl").Trim();
                string note = DataGrid1.GetControlValue(row, "note").Trim();
                string logo = DataGrid1.GetControlValue(row, "logo").Trim();
                if (SASLinks.UpdateSASLink(int.Parse(o.ToString()), displayorder, name, url, note, logo) == -1)
                    error = true;
                else
                    row++;
            }
            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "批量更新友情链接", "");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");
            if (error)
                base.RegisterStartupScript("PAGE", "alert('某些信息不完整，未能更新！');window.location.href='global_forumlinksgrid.aspx';");
            else
                base.RegisterStartupScript("PAGE", "window.location.href='global_forumlinksgrid.aspx';");
            #endregion
        }


        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region 设置绑定相关数据的长度

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox t = (TextBox)e.Item.Cells[2].Controls[0];
                t.Attributes.Add("maxlength", "6");
                t.Width = 40;

                t = (TextBox)e.Item.Cells[3].Controls[0];
                t.Attributes.Add("maxlength", "100");
                t.Width = 80;

                t = (TextBox)e.Item.Cells[4].Controls[0];
                t.Attributes.Add("maxlength", "100");
                t.Width = 120;

                t = (TextBox)e.Item.Cells[5].Controls[0];
                t.Attributes.Add("maxlength", "200");
                t.Width = 300;

                t = (TextBox)e.Item.Cells[6].Controls[0];
                t.Attributes.Add("maxlength", "100");
                t.Width = 100;
            }

            #endregion
        }


        private void DelRec_Click(object sender, EventArgs e)
        {
            #region 删除选定的友情链接
            if (this.CheckCookie())
            {
                if (SASRequest.GetString("delid") != "")
                {
                    SASLinks.DeleteSASLink(SASRequest.GetString("delid"));
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip,
                        "删除友情链接", "删除友情链接,ID为: " + SASRequest.GetString("id").Replace("0 ", ""));
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");
                    Response.Redirect("global_forumlinksgrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项');window.location.href='global_forumlinksgrid.aspx';</script>");
                }
            }

            #endregion
        }


        public string LogoStr(string filename)
        {
            #region 设置友情链接的LOGO显示

            if (filename.Trim() != "")
            {
                if (filename.ToLower().StartsWith("http"))
                {
                    return "<div align=\"left\"><img src=\"" + filename + "\" width=\"91px\" height=\"32px\"/></div>";
                }
                else
                {
                    return "<div align=\"left\"><img src=\"" + BaseConfigs.GetSitePath + filename + "\" width=\"91px\" height=\"32px\"/></div>";
                }
            }
            else
            {
                return "";
            }

            #endregion
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DelRec.Click += new EventHandler(this.DelRec_Click);
            this.SaveFriend.Click += new EventHandler(this.SaveFriend_Click);
            DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            DataGrid1.ColumnSpan = 8;
        }
        #endregion
    }
}
