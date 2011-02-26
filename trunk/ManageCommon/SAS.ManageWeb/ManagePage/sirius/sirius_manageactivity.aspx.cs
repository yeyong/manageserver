using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Cache;
using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.Sirius;

namespace SAS.ManageWeb.ManagePage
{
    public partial class sirius_manageactivity : AdminPage
    {
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();
        private int tid = SASRequest.GetInt("tid", 0);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (tid < 1)
                {
                    base.RegisterStartupScript("", "<script>alert('页面参数错误！');window.location.href='sirius_searchactivity.aspx';</script>");
                    return;
                }
                BindData();
            }
        }

        public void BindData()
        {
            #region 绑定用户组列表
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "团队活动列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "id";
            DataGrid1.BindData(spb.GetTeamActByTid(tid));
            DataGrid1.Sort = "id";
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

        protected void EditUserGroup_Click(object sender, EventArgs e)
        {
            #region 编辑团队信息

            int row = 0;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                int tID = int.Parse(o.ToString());
                TeamActInfo team = spb.GetTeamActInfo(tID);

                string tName = DataGrid1.GetControlValue(row, "name");

                if (tName.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队活动名称不可为空，请认真填写!');window.location.href='sirius_manageactivity.aspx?tid=" + team.Teamid + "';</script>");
                    return;
                }

                team.Name = tName;
                spb.UpdateTeamAct(team);
                base.RegisterStartupScript("PAGE", "window.location.href='sirius_manageactivity.aspx?tid=" + team.Teamid + "';");
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
            DataGrid1.DataKeyField = "id";
            DataGrid1.ColumnSpan = 12;
            this.EditUserGroup.Click += new EventHandler(EditUserGroup_Click);
        }
        #endregion
    }
}
