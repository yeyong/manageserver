using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_goodsbrandgrid : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            #region 绑定品牌列表
            DataGrid1.VirtualItemCount = GetBrandCount();
            DataGrid1.DataSource = BuildBrandData();
            DataGrid1.DataBind();
            #endregion
        }

        /// <summary>
        /// 绑定品牌数据
        /// </summary>
        public DataTable BuildBrandData()
        {
            DataTable dt = new DataTable();
            if (ViewState["condition"] == null)
            {
                dt = tpb.GetGoodsBrandByPage("", DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1);
            }
            else
            {
                dt = tpb.GetGoodsBrandByPage(ViewState["condition"].ToString(), DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1);
            }
            return dt;
        }
        /// <summary>
        /// 品牌数量
        /// </summary>
        /// <returns></returns>
        public int GetBrandCount()
        {
            if (ViewState["condition"] == null) return tpb.GetGoodsBrandCountByCond("1=1");
            else return tpb.GetGoodsBrandCountByCond(ViewState["condition"].ToString());
        }

        private void ENStart_Click(object sender, EventArgs e)
        {
            #region 开启操作
            if (SASRequest.GetString("enid") != "")
            {
                string enidlist = SASRequest.GetString("enid");
                tpb.SetGoodsBrandListStart(enidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台开启品牌", "品牌名:批量开启 " + enidlist);
                BindData();
            }
            #endregion
        }

        private void ENPause_Click(object sender, EventArgs e)
        {
            #region 暂停操作
            if (SASRequest.GetString("enid") != "")
            {
                string enidlist = SASRequest.GetString("enid");
                tpb.SetGoodsBrandListStop(enidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台暂停品牌", "品牌名:批量暂停 " + enidlist);
                BindData();
            }
            #endregion
        }

        private void Search_Click(object sender, EventArgs e)
        {
            #region 按指定条件查询用户数据

            if (this.CheckCookie())
            {
                string searchcondition = tpb.GetGoodsBrandSearchCondition(islike.Checked, brandname.Text.Trim(), relateclass.SelectedValue, TypeConverter.StrToInt(enstatus.SelectedValue));
                ViewState["condition"] = searchcondition;
                searchtable.Visible = false;
                ResetSearchTable.Visible = true;
                DataGrid1.CurrentPageIndex = 0;
                BindData();
            }

            #endregion
        }

        private void ResetSearchTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("taobao_goodsbrandgrid.aspx");
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

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            DataGrid1.AllowCustomPaging = true;
            DataGrid1.TableHeaderName = "品牌信息列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "id";
            DataGrid1.ColumnSpan = 12;
            DataGrid1.GoToPagerButton.Click += new EventHandler(GoToPagerButton_Click);
            ENStart.Click += new EventHandler(ENStart_Click);
            ENPause.Click += new EventHandler(ENPause_Click);
            Search.Click += new EventHandler(Search_Click);
            ResetSearchTable.Click += new EventHandler(ResetSearchTable_Click);
            relateclass.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
        }

        #endregion
    }
}
