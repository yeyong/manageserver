using System;
using System.Web;
using System.Data;

using SAS.Logic;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage.company
{
    /// <summary>
    /// ajax异步操作
    /// </summary>
    public class global_ajaxcall : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string resultmessage = "";
                switch (Request.Params["opname"])
                {
                    case "area1":
                        resultmessage = areas.returnProvinces();
                        break;
                    case "area2":
                        int parentid = SASRequest.GetInt("parentcode",0);
                        resultmessage = areas.returnCity(parentid);
                        break;
                    case "area3":
                        int parentid1 = SASRequest.GetInt("parentcode", 0);
                        resultmessage = areas.returnDistrict(parentid1);
                        break;
                }
                Response.Write(resultmessage);
                Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
                Response.Expires = -1;
                Response.End();
            }

        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}
