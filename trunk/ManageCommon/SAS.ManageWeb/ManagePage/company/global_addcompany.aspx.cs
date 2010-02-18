using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_addcompany : AdminPage
    {
        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            //provice.InnerText = areas.returnProvinces();
            //this.SubmitAdd.Click += new EventHandler(this.SubmitAdd_Click);            
        }

        #endregion        
    }
}
