using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

using SAS.Common;
using SAS.Config;
using SAS.Control;
using SAS.Logic;
using SAS.Entity;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace SAS.ManageWeb.ManagePage
{
    public partial class company_cardconfiggrid : AdminPage
    {
        private DataTable cardconfigTable = CardConfigs.GetCardConfigList();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataGrid1.DataKeyField = "id";
            string menuid = SASRequest.GetString("ctid");
            string mode = SASRequest.GetString("mode");

            if (mode != "")
            {
                if (mode == "del")
                {
                    CardConfigs.DeleteCardConfig(SASRequest.GetQueryInt("id", 0));
                }
                else
                {
                    if (SASRequest.GetFormString("name").Trim() == "" || SASRequest.GetFormString("displayorder").Trim() == "" || SASRequest.GetFormInt("displayorder", 0) > Int16.MaxValue)
                    {
                        this.RegisterStartupScript("", "<script type='text/javascript'>alert('名称或序号输入不合法。');window.location=window.location;</script>");
                        return;
                    }
                    if (menuid == "0")
                    {
                        CardConfigInfo cci = new CardConfigInfo();
                        GetFromData(cci);
                        CardConfigs.InsertCardConfig(cci);

                    }
                    else
                    {
                        CardConfigInfo cci = new CardConfigInfo();
                        cci.id = SASRequest.GetFormInt("ctid", 0);
                        GetFromData(cci);
                        CardConfigs.UpdateCardConfig(cci);
                    }
                }
                Response.Redirect(Request.RawUrl, true);
            }
            else
            {
                BindDataGrid();
            }
        }

        private void GetFromData(CardConfigInfo cci)
        {
            
        }

        private void BindDataGrid()
        {
            DataGrid1.DataSource = cardconfigTable;
            DataGrid1.DataBind();
        }
    }
}
