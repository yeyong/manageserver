using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    public partial class commentedit : AdminPage
    {
        protected int objid = SASRequest.GetInt("objid", 0);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Companys comp = Companies.GetCompanyInfo(objid);
                if (comp != null) PageInfo1.Text = comp.En_name;
                BindData();
            }
        }

        public void BindData()
        {
            #region 绑定企业列表
            DataGrid1.VirtualItemCount = Comments.GetCommentCountByQyID(objid);
            DataGrid1.DataSource = Comments.GetCommentListByQyID(objid, DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1);
            DataGrid1.DataBind();
            #endregion
        }

        #region GridView操作
        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
            BindData();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }

        public void GoToPagerButton_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #endregion

        private void ENPause_Click(object sender, EventArgs e)
        {
            #region 删除操作
            if (SASRequest.GetString("commid") != "")
            {
                string commidlist = SASRequest.GetString("commid");
                int delcount = Comments.DelComments(commidlist);
                if ( delcount> 0)
                {
                    AdminCompanies.UpdateCompanyCommentCount(objid, -delcount);
                }
                BindData();
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
            DataGrid1.AllowCustomPaging = true;
            DataGrid1.TableHeaderName = "评论信息列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "commentid";
            DataGrid1.ColumnSpan = 12;
            DataGrid1.GoToPagerButton.Click += new EventHandler(GoToPagerButton_Click);

            ENPause.Click += new EventHandler(ENPause_Click);
        }

        #endregion
    }
}
