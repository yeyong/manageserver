using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Control;
using SAS.Config;
using SAS.Entity;
using SAS.Common;
using SAS.Logic;
using SAS.Entity.Domain;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class toabao_editshopproduct : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected System.Collections.Generic.List<ItemCat> icatlist = new System.Collections.Generic.List<ItemCat>();
        protected ShopDetailInfo sdinfo = new ShopDetailInfo();
        protected string sid = SASRequest.GetString("sid");
        protected string selectitems = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (sdinfo == null)
                {
                    base.RegisterStartupScript("", "<script>alert('参数传递错误！');window.location.href='taobao_collectionshop.aspx';</script>");
                    return;
                }

                selectitems = "," + sdinfo.relategoods;
            }
        }

        protected void EditRecommendInfo_Click(object sender, EventArgs e)
        {
            string thecontent = SASRequest.GetString("selitems").Trim().Trim(',');

            string errmsg = "";
            if (thecontent == "")
            {
                errmsg = "推荐内容不可为空！";
            }

            if (errmsg != "")
            {
                base.RegisterStartupScript("", "<script>alert('" + errmsg + "');window.location.href='toabao_editshopproduct.aspx?sid=" + sid + "';</script>");
                return;
            }
            tpb.UpdateTaoBaoShopProduct(long.Parse(sid),Utils.ClearBR(thecontent));
            base.RegisterStartupScript("PAGE", "window.location.href='taobao_collectionshop.aspx';");
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            EditRecommendInfo.Click += new EventHandler(EditRecommendInfo_Click);
            icatlist = tpb.GetItemCatCache(0);
            sdinfo = tpb.GetTaoBaoShopInfo(sid);
        }

        #endregion
    }
}
