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

namespace SAS.Sirius.Admin
{
    public partial class manageteam : AdminPage
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
            #region 绑定用户组列表
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "团队列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "teamID";
            DataGrid1.BindData(Sirius.GetAllTeamInfoList());
            DataGrid1.Sort = "createDate";
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
                TeamInfo team = Sirius.GetTeamInfoByTeamID(tID);

                string tName = DataGrid1.GetControlValue(row, "name");

                if (tName.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队名称不可为空，请认真填写!');window.location.href='sirius_manageteam.aspx';</script>");
                    return;
                }
                string tUrl = DataGrid1.GetControlValue(row, "teamdomain");
                if (tUrl.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队域名是团队的重要标识，不可为空，请认真填写!');window.location.href='sirius_manageteam.aspx';</script>");
                    return;
                }
                if (!Utils.IsURL(tUrl))
                {
                    base.RegisterStartupScript("", "<script>alert('团队域名是团队的重要标识，要符合格式要求，例:http://www.sirius.org.cn');window.location.href='sirius_manageteam.aspx';</script>");
                    return;
                }
                string tOrder = DataGrid1.GetControlValue(row, "displayorder");
                if (tOrder.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队显示顺序不可为空，请认真填写!');window.location.href='sirius_manageteam.aspx';</script>");
                    return;
                }
                if (!Utils.IsNumeric(tOrder))
                {
                    base.RegisterStartupScript("", "<script>alert('团队显示顺序应为数字，请认真填写!');window.location.href='sirius_manageteam.aspx';</script>");
                    return;
                }

                team.Name = tName;
                team.Teamdomain = tUrl;
                team.Displayorder = int.Parse(tOrder);

                string members = "";
                Sirius.UpdateTeamInfo(team,out members);
                base.RegisterStartupScript("PAGE", "window.location.href='sirius_manageteam.aspx';");
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
            DataGrid1.DataKeyField = "teamID";
            DataGrid1.ColumnSpan = 12;
            this.EditUserGroup.Click += new EventHandler(EditUserGroup_Click);
        }
        #endregion
    }
}
