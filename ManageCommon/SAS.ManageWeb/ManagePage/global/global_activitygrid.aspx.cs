using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 活动专题管理
    /// </summary>
    public partial class global_activitygrid : AdminPage
    {
        public string condition = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["activitieswhere"] != null)
                {
                    condition = Session["activitieswhere"].ToString();
                }
                else
                {
                    Response.Redirect("global_searchactivity.aspx");
                    return;
                }
                BindData();
            }
        }

        public void BindData()
        {
            #region
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData(Activities.GetActivitiesByConditions(condition));
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

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            DataGrid1.TableHeaderName = "活动专题列表";
            DataGrid1.ColumnSpan = 11;
            DataGrid1.DataKeyField = "id";
        }

        #endregion
    }
}
