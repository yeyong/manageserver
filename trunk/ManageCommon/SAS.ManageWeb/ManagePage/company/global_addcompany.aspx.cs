using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_addcompany : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }

        private void AddCompanyInfo_Click(object sender, EventArgs e)
        {

        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            this.AddCompanyInfo.Click += new EventHandler(AddCompanyInfo_Click);
        }

        #endregion        
    }
}
