using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Plugin.TaoBao;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_recommendgrid : AdminPage
    {
        protected int rtype = SASRequest.GetInt("ctype", 0);
        protected string rtypestr = "";
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (rtype == 0)
            {
                base.RegisterStartupScript("", "<script>alert('页面异常，请与管理员联系!');window.location.href='taobao_editrecommend.aspx';</script>");
                return;
            }

            switch (rtype)
            {
                case 1: rtypestr = "商品"; break;
                case 2: rtypestr = "店铺"; break;
                case 3: rtypestr = "活动"; break;
                case 4: rtypestr = "频道"; break;
            }

            if (!Page.IsPostBack)
            {
                joindateStart.SelectedDate = System.DateTime.Now.AddDays(-30);
                joindateEnd.SelectedDate = System.DateTime.Now;
                updatedateStart.SelectedDate = System.DateTime.Now.AddDays(-30);
                updatedateEnd.SelectedDate = System.DateTime.Now;
                BindData();
            }
        }

        public void BindData()
        {
            #region 绑定企业列表
            string conditions = "";

            if (ViewState["condition"] != null)
            {
                conditions = ViewState["condition"].ToString();
            }
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData(tpb.GetRecommendsByCond(conditions));
            #endregion
        }

        private void Search_Click(object sender, EventArgs e)
        {
            #region 按指定条件查询用户数据

            if (this.CheckCookie())
            {
                string searchcondition = tpb.GetRecommendCondition(islike.Checked, rtitle.Text.Trim(), TypeConverter.ObjectToInt(rcategory.SelectedValue, -1), TypeConverter.ObjectToInt(rchanel.SelectedValue, -1), isbuilddatetime.Checked, joindateStart.SelectedDate.ToString(), joindateEnd.SelectedDate.ToString(), isupdatedatetime.Checked, updatedateStart.SelectedDate.ToString(), updatedateEnd.SelectedDate.ToString());
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
            Response.Redirect("taobao_recommendgrid.aspx?ctype=" + rtype);
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
            DataGrid1.TableHeaderName = rtypestr + "推荐信息列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "id";
            DataGrid1.ColumnSpan = 12;
            DataGrid1.GoToPagerButton.Click += new EventHandler(GoToPagerButton_Click);
            Search.Click += new EventHandler(Search_Click);
            ResetSearchTable.Click += new EventHandler(ResetSearchTable_Click);

            TaoChanel ete = new TaoChanel();
            rchanel.Items.Clear();
            rchanel.Items.Add(new ListItem("请选择频道", "-1"));
            foreach (string cname in Enum.GetNames(ete.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ete.GetType(), cname));
                string s_text = EnumCatch.GetTaoChanel(s_value);
                rchanel.Items.Add(new ListItem(s_text, s_value.ToString()));
            }

            rcategory.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
        }

        #endregion
    }
}
