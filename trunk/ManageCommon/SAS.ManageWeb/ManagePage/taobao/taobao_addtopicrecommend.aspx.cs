using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Config;
using SAS.Entity;
using SAS.Common;
using SAS.Logic;
using SAS.Entity.Domain;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_addtopicrecommend : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        /// <summary>
        /// 推荐类型1，商品推广；2，店铺推广;3，活动推广
        /// </summary>
        private int rtype = 3;
        protected string taobaouserid = "";

        protected void AddRecommendInfo_Click(object sender, EventArgs e)
        {
            string thertitle = rtitle.Text.Trim();
            int thercategory = TypeConverter.ObjectToInt(rcategory.SelectedValue, 0);
            int therchanel = TypeConverter.ObjectToInt(rchanel.SelectedValue, 0);
            string thecontent = SASRequest.GetString("selitems").Trim().Trim(',');

            string errmsg = "";
            if (thertitle == "")
            {
                errmsg = "推荐标题不可为空，请仔细填写！";
            }
            if (thecontent == "")
            {
                errmsg = "推荐内容不可为空！";
            }

            if (errmsg != "")
            {
                base.RegisterStartupScript("", "<script>alert('" + errmsg + "');window.location.href='taobao_addtopicrecommend.aspx';</script>");
                return;
            }

            if (tpb.CreateRecommendInfo(thercategory, therchanel, thertitle, thecontent, rtype) > 0)
            {
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/RecommendList");
                base.RegisterStartupScript("PAGE", "window.location.href='taobao_recommendGrid.aspx?ctype=" + rtype + "';");
            }
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
            taobaouserid = taobaoconfig.UserID;
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
