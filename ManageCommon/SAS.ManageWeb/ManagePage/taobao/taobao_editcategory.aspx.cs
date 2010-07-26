using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Entity;
using SAS.Common;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_editcategory : AdminPage
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        protected int cid = SASRequest.GetInt("cid", 0);
        protected TaoBaoPluginBase tbp = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (cid <= 0)
                {
                    base.RegisterStartupScript("", "<script>alert('类别异常，请与管理员联系!');window.location.href='taobao_categorygrid.aspx';</script>");
                    return;
                }
                CategoryInfo curinfo = tbp.GetCategoryInfo(cid);

                if (curinfo == null)
                {
                    base.RegisterStartupScript("", "<script>alert('类别异常，请与管理员联系!');window.location.href='taobao_categorygrid.aspx';</script>");
                    return;
                }

                cname.Text = curinfo.Name.Trim();
                displayorder.Text = curinfo.Displayorder.ToString();
                available.SelectedValue = curinfo.Cg_status.ToString();
            }
        }

        private void AddCategoryInfo_Click(object sender, EventArgs e)
        {
            CategoryInfo cinfo = tbp.GetCategoryInfo(cid);
            cinfo.Name = Utils.RemoveHtml(cname.Text.Trim());
            cinfo.Displayorder = TypeConverter.ObjectToInt(displayorder.Text, 0);
            cinfo.Cg_status = TypeConverter.ObjectToInt(available.SelectedValue, 0);
            cinfo.Cg_relateclass = SASRequest.GetString("TargetFID");

            tbp.UpdateCategoryInfo(cinfo);
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CategoryList");
            base.RegisterStartupScript("PAGE", "window.location.href='taobao_categorygrid.aspx';");
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.AddCategoryInfo.Click += new EventHandler(AddCategoryInfo_Click);
        }

        #endregion 
    }
}
