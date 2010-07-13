using System;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

using SAS.Logic;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    public partial class ajax : System.Web.UI.Page
    {
        protected internal string ascxpath = "UserControls/"; //用户控件路径值

        private void InitializeComponent()
        {
            this.ID = "Ajax_CallBack_Form";
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (base.Request.Params["AjaxTemplate"] != null)
            {
                try
                {
                    this.AjaxCallBackForm.Controls.Add(base.LoadControl(base.Request.Params["AjaxTemplate"].ToLower().EndsWith(".ascx") ? ascxpath + base.Request.Params["AjaxTemplate"] : (ascxpath + base.Request.Params["AjaxTemplate"] + ".ascx")));
                }
                catch
                {
                }
            }
        }
    }
}
