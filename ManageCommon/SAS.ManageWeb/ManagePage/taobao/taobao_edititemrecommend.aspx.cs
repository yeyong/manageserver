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
    public partial class taobao_edititemrecommend : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected System.Collections.Generic.List<ItemCat> icatlist = new System.Collections.Generic.List<ItemCat>();
        private int rtype = 1;
        protected int rid = SASRequest.GetInt("id", 0);
        protected string selectitems = "";
        protected System.Collections.Generic.List<TaobaokeItem> taobaokeitemlist = new System.Collections.Generic.List<TaobaokeItem>();
        protected RecommendInfo rinfo = new RecommendInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                icatlist = tpb.GetItemCatCache(0);
                if (rinfo == null)
                {
                    base.RegisterStartupScript("", "<script>alert('参数传递错误！');window.location.href='taobao_additemrecommend.aspx';</script>");
                    return;
                }
                taobaokeitemlist = tpb.GetTaoBaoKeItemList(rinfo.ccontent);

                rtitle.Text = rinfo.ctitle;
                rchanel.SelectedValue = rinfo.relatechanel.ToString();
                rcategory.SelectedValue = rinfo.relatecategory.ToString();
                selectitems = "," + rinfo.ccontent;
            }
        }

        protected void EditRecommendInfo_Click(object sender, EventArgs e)
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
                base.RegisterStartupScript("", "<script>alert('" + errmsg + "');window.location.href='taobao_additemrecommend.aspx';</script>");
                return;
            }

            tpb.UpdateRecommendInfo(rid, thercategory, therchanel, thertitle, thecontent, rtype);
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/RecommendWithItem/Chanel_" + rinfo.relatechanel + "/Class_" + rinfo.relatecategory, true);
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/RecommendWithItem/Chanel_" + therchanel + "/Class_" + thercategory, true);
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/RecommendItem/Chanel_" + rinfo.relatechanel + "/Class_" + rinfo.relatecategory, true);
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/RecommendItem/Chanel_" + therchanel + "/Class_" + thercategory, true);
            base.RegisterStartupScript("PAGE", "window.location.href='taobao_recommendgrid.aspx?ctype=" + rtype + "';");
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

            TaoChanel ete = new TaoChanel();
            rchanel.Items.Clear();
            foreach (string cname in Enum.GetNames(ete.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ete.GetType(), cname));
                string s_text = EnumCatch.GetTaoChanel(s_value);
                rchanel.Items.Add(new ListItem(s_text, s_value.ToString()));
            }

            rcategory.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
            rinfo = tpb.GetRecommendInfo(rid);
        }

        #endregion
    }
}
