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
    public partial class taobao_additemrecommend : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected System.Collections.Generic.List<ItemCat> icatlist = new System.Collections.Generic.List<ItemCat>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                icatlist = tpb.GetItemCatCache(0);
            }
        }

        protected void AddRecommendInfo_Click(object sender, EventArgs e)
        {

        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            AddRecommendInfo.Click += new EventHandler(AddRecommendInfo_Click);

            TaoChanel ete = new TaoChanel();
            rchanel.Items.Clear();
            foreach (string cname in Enum.GetNames(ete.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ete.GetType(), cname));
                string s_text = EnumCatch.GetTaoChanel(s_value);
                rchanel.Items.Add(new ListItem(s_text, s_value.ToString()));
            }

            rcategory.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
        }

        #endregion
    }
}
