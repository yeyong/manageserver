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
    public partial class sirius_managework : AdminPage
    {
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();
        private int tid = SASRequest.GetInt("tid", 0);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (tid < 1)
                {
                    base.RegisterStartupScript("", "<script>alert('页面参数错误！');window.location.href='sirius_searchwork.aspx';</script>");
                    return;
                }
                BindData();
            }
        }

        public void BindData()
        {
            #region 绑定用户组列表
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "团队成果列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "id";
            DataGrid1.BindData(spb.GetTeamWorkByTid(tid));
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
                TeamWorkInfo team = spb.GetWorkInfo(tID);

                string tName = DataGrid1.GetControlValue(row, "name");

                if (tName.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队成果名称不可为空，请认真填写!');window.location.href='sirius_managework.aspx?tid=" + team.Teamid + "';</script>");
                    return;
                }
                string tUrl = DataGrid1.GetControlValue(row, "url");
                if (tUrl.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('成果链接是成果的重要标识，不可为空，请认真填写!');window.location.href='sirius_managework.aspx?tid=" + team.Teamid + "';</script>");
                    return;
                }
                if (!Utils.IsURL(tUrl))
                {
                    base.RegisterStartupScript("", "<script>alert('成果链接是成果的重要标识，要符合格式要求，例:http://www.sirius.org.cn');window.location.href='sirius_managework.aspx?tid=" + team.Teamid + "';</script>");
                    return;
                }

                team.Name = tName;
                team.Url = tUrl;
                string result = "";
                spb.UpdateWorkInfo(team, out result);
                base.RegisterStartupScript("PAGE", "window.location.href='sirius_managework.aspx?tid=" + team.Teamid + "';");
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
