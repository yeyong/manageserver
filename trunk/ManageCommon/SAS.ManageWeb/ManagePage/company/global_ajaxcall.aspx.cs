using System;
using System.Web;
using System.Data;

using SAS.Logic;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// ajax异步操作
    /// </summary>
    public class global_ajaxcall : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int parentid = SASRequest.GetInt("parentcode", 0);

            if (!IsPostBack)
            {
                string resultmessage = "";
                switch (Request.Params["opname"])
                {
                    case "area1":
                        resultmessage = areas.returnProvinces(SASRequest.GetString("defvalue"));
                        break;
                    case "area2":
                        resultmessage = areas.returnCity(SASRequest.GetString("defvalue"), parentid);
                        break;
                    case "area3":
                        resultmessage = areas.returnDistrict(SASRequest.GetString("defvalue"), parentid);
                        break;
                    case "catalog":
                        resultmessage = Catalogs.ReturnCalalogList(SASRequest.GetInt("parentid", 0));
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
