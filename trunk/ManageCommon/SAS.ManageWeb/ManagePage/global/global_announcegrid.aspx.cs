using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_announcegrid : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData(Logic.Announcements.GetAnnouncementList());
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        private void DelRec_Click(object sender, EventArgs e)
        {
            #region 删除公告

            if (CheckCookie())
            {
                if (SASRequest.GetString("id") != "")
                {
                    Logic.Announcements.DeleteAnnouncements(SASRequest.GetString("id"));
                    AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "删除公告", "删除公告,公告ID为: " + SASRequest.GetString("id"));
                    Response.Redirect("global_announcegrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项');window.location.href='global_announcegrid.aspx';</script>");
                }
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
            DelRec.Click += new EventHandler(DelRec_Click);

            DataGrid1.TableHeaderName = "公告列表";
            DataGrid1.ColumnSpan = 7;
        }

        #endregion
    }
}
