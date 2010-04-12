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

        protected DataTable cardtemplist = Templates.GetValidCardTemplateList();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataGrid1.DataKeyField = "id";
            string ccid = SASRequest.GetString("id");
            string mode = SASRequest.GetString("mode");

            if (mode != "")
            {
                if (mode == "del")
                {
                    CardConfigs.DeleteCardConfig(SASRequest.GetQueryInt("id", 0));
                }
                else
                {
                    if (SASRequest.GetFormString("ccname").Trim() == "" || Utils.IsDateString(SASRequest.GetFormString("createdate")))
                    {
                        this.RegisterStartupScript("", "<script type='text/javascript'>alert('名称或有效期输入不合法。');window.location=window.location;</script>");
                        return;
                    }
                    if (ccid == "0")
                    {
                        CardConfigInfo cci = new CardConfigInfo();
                        GetFromData(cci);
                        CardConfigs.InsertCardConfig(cci);

                    }
                    else
                    {
                        CardConfigInfo cci = new CardConfigInfo();
                        cci.id = SASRequest.GetFormInt("id", 0);
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
            cci.ccname = SASRequest.GetFormString("ccname");
            cci.tid = SASRequest.GetFormInt("tid", 1);
            cci.hasflash = SASRequest.GetFormInt("hasflash",0);
            cci.hasimage = SASRequest.GetFormInt("hasimage", 0);
            cci.hasjs = SASRequest.GetFormInt("hasjs", 0);
            cci.hassilverlight = SASRequest.GetFormInt("hassilverlight", 0);
            cci.showparams = SASRequest.GetFormString("showparams");
            cci.vailddate = SASRequest.GetFormString("vailddate");
        }

        private void BindDataGrid()
        {
            DataGrid1.DataSource = cardconfigTable;
            DataGrid1.DataBind();
            string cardscript = "\r\n<script type='text/javascript'>\r\nnav = [";
            foreach (DataRow dr in cardconfigTable.Rows)
            {
                cardscript += String.Format("\r\n{{id:'{0}',ccname:'{1}',tid:'{2}',hasflash:'{3}',hasimage:'{4}',hasjs:'{5}',hassilverlight:'{6}',showparams:'{7}',createdate:'{8}',vailddate:'{9}'}},",
                    dr["id"], dr["ccname"], dr["tid"], dr["hasflash"], dr["hasimage"], dr["hasjs"], dr["hassilverlight"], dr["showparams"], dr["createdate"], dr["vailddate"]);
            }
            cardscript = cardscript.TrimEnd(',') + "];\r\n</script>";
            this.RegisterStartupScript("", cardscript);
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region 数据绑定显示长度控制

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox t = (TextBox)e.Item.Cells[0].Controls[0];
                t.Attributes.Add("size", "4");

                t = (TextBox)e.Item.Cells[7].Controls[0];
                t.Attributes.Add("size", "20");
            }

            #endregion
        }

        protected string GetDeleteLink(string id)
        {
            if (cardconfigTable.Select("id=" + id).Length == 0)
            {
                return String.Format("<a href=\"?mode=del&id={0}\" onclick=\"return confirm('确认要将该菜单项删除吗?');\">删除</a>", id);
            }
            return "";
        }

        protected void saveNav_Click(object sender, EventArgs e)
        {
            int row = 0;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                int id = int.Parse(o.ToString());
                string ccname = DataGrid1.GetControlValue(row, "ccname").Trim();
                string vailddate = DataGrid1.GetControlValue(row, "vailddate").Trim();
                CardConfigInfo cci = CardConfigs.GetCardConfigInfo(id);
                if (cci == null)
                    continue;
                if (!Utils.IsDateString(vailddate) || ccname == "")
                {
                    row++;
                    continue;
                }

                cci.ccname = ccname;
                cci.vailddate = vailddate;
                CardConfigs.UpdateCardConfig(cci);

                row++;
            }
            Response.Redirect(Request.RawUrl, true);
        }
    }
}
