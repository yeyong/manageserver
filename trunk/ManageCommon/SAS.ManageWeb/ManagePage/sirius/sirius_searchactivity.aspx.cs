using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Control;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Common;
using SAS.Plugin.Sirius;

namespace SAS.ManageWeb.ManagePage
{
    public partial class sirius_searchactivity : AdminPage
    {
        SiriusPluginBase spb = SiriusPluginProvider.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void SaveSearchCondition_Click(object sender, EventArgs e)
        {
            #region 生成查询条件

            if (this.CheckCookie())
            {
                Response.Redirect("sirius_manageactivity.aspx?tid=" + teamsel.SelectedValue);
            }

            #endregion
        }

        #region 把VIEWSTATE写入容器

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SASLogicSavePageState(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            return base.DiscuzForumLoadPageState();
        }

        #endregion

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveSearchCondition.Click += new EventHandler(this.SaveSearchCondition_Click);
            
            teamsel.Items.Clear();
            teamsel.Items.Add(new ListItem("全部", "0"));
            teamsel.AddTableData(spb.GetAllTeam(), "name", "teamid");
        }

        #endregion
    }
}
