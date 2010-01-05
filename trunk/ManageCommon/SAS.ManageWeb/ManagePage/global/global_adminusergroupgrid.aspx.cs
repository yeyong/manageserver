using System;
using System.Web.UI.WebControls;

using SAS.Logic;
using DataGrid = SAS.Control.DataGrid;
using SAS.Config;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 管理用户组列表
    /// </summary>
    public partial class global_adminusergroupgrid : AdminPage
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
            DataGrid1.TableHeaderName = "管理组列表";
            DataGrid1.DataKeyField = "ug_id";
            DataGrid1.BindData(UserGroups.GetAdminUserGroup());
            DataGrid1.Sort = "ug_id";
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
