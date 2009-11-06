using System;
using System.Web.UI.WebControls;

using SAS.Logic;
using DataGrid = SAS.Control.DataGrid;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_sysadminusergroupgrid : AdminPage
    {
        #region 控件声明

        protected CheckBox chkConfirmInsert;
        protected CheckBox chkConfirmUpdate;
        protected CheckBox chkConfirmDelete;

        #endregion

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
            DataGrid1.TableHeaderName = "系统组列表";
            List<UserGroupInfo> list = new List<UserGroupInfo>();
            foreach (UserGroupInfo userGroupInfo in UserGroups.GetUserGroupList())
            {
                if (userGroupInfo.ug_isSystem == 1)
                    list.Add(userGroupInfo);
            }
            DataGrid1.BindData(list);
        }

        protected string GetLink(string radminid, string groupid)
        {
            int adminId = int.Parse(radminid);
            if (adminId > 0 && adminId <= 3)
            {
                return "global_editadminusergroup.aspx?groupid=" + groupid;
            }
            else
            {
                return "global_editsysadminusergroup.aspx?groupid=" + groupid;
            }
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            DataGrid1.DataKeyField = "ug_id";
            DataGrid1.ColumnSpan = 12;
        }

        #endregion
    }
}
