using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class company_companygrid : AdminPage
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
            DataGrid1.TableHeaderName = "企业信息列表";
            DataGrid1.Attributes.Add("borderStyle", "2");
            DataGrid1.DataKeyField = "en_id";
            DataGrid1.BindData(AdminCompanies.GetCompanyList());
            #endregion
        }

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <param name="enstatus"></param>
        /// <returns></returns>
        protected string GetStatusName(string enstatus)
        {
            string statusname = "";
            int _status = Utils.StrToInt(enstatus, 0);
            switch (_status)
            {
                case 0:
                    statusname = "审核未通过";
                    break;
                case 1:
                    statusname = "服务审核中";
                    break;
                case 2:
                    statusname = "审核通过";
                    break;
                default:
                    statusname = "审核未通过";
                    break;
            }

            return statusname;
        }

        private void LocationSet_Click(object sender, EventArgs e)
        {
            areas.GetInstance.WriteJsonFile();
        }
       
        private void ENStart_Click(object sender, EventArgs e)
        {
            #region 开启操作
            if (SASRequest.GetString("enid") != "")
            {
                string enidlist = SASRequest.GetString("enid");
                AdminCompanies.StartCompany(enidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台开启企业", "企业名:批量开启 " + enidlist);
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
                AdminCompanies.PauseCompany(enidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台暂停企业", "企业名:批量暂停 " + enidlist);
                BindData();
            }
            #endregion
        }

        #region GridView操作
        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
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
            DataGrid1.DataKeyField = "en_id";
            DataGrid1.ColumnSpan = 12;
            ENStart.Click += new EventHandler(ENStart_Click);
            ENPause.Click += new EventHandler(ENPause_Click);
            LocationSet.Click += new EventHandler(LocationSet_Click);
        }

        #endregion
    }
}
