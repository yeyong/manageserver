using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Entity;

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
            DataGrid1.BindData(AdminActivities.GetActivitiesByConditions(condition));
            #endregion
        }

        public string GetActivityType(string atype)
        {
            if (atype == "0") return "全部";
            return EnumCatch.GetActivityType(TypeConverter.StrToInt(atype, 0));
        }

        public string BoolStr(string enabled)
        {
            return enabled == "1" ? "<div align=center><img src=../images/OK.gif /></div>" : "<div align=center><img src=../images/Cancel.gif /></div>";
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        private void SetActInfo_Click(object sender, EventArgs e)
        {
            #region 按选定的操作管理活动专题

            if (this.CheckCookie())
            {
                if (SASRequest.GetString("id") != "")
                {
                    string idlist = SASRequest.GetString("id");
                    switch (SASRequest.GetString("operation"))
                    {
                        case "movetype":
                            AdminActivities.SetActivityType(idlist, TypeConverter.StrToInt(typeid.SelectedValue, 0));
                            break;
                        case "allenabled":
                            AdminActivities.SetActivityEnabled(idlist);
                            break;
                        case "allunabled":
                            AdminActivities.SetActivityUnabled(idlist);
                            break;
                        case "deleteact":
                            AdminActivities.DeleteActivities(idlist);
                            break;
                    }
                    if (TypeConverter.StrToInt(typeid.SelectedValue, 0) == Convert.ToInt16(ActivityType.TaobaoActivity))
                    {
                        SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TaoActivities");
                        //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoActivities", true);
                    }
                    base.RegisterStartupScript("PAGE", "window.location.href='global_activitygrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('请选择相应的专题!');window.location.href='global_activitygrid.aspx';</script>");
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
            DataGrid1.TableHeaderName = "活动专题列表";
            DataGrid1.ColumnSpan = 11;
            DataGrid1.DataKeyField = "id";
            SetActInfo.Click += new EventHandler(SetActInfo_Click);

            ActivityType ate = new ActivityType();
            typeid.Items.Clear();
            typeid.Items.Add(new ListItem("全部", "0"));
            foreach (string atname in Enum.GetNames(ate.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ate.GetType(), atname));
                string s_text = EnumCatch.GetActivityType(s_value);
                typeid.Items.Add(new ListItem(s_text, s_value.ToString()));
            }
        }

        #endregion
    }
}
