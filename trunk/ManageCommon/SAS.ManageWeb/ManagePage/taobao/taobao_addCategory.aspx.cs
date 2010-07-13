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
    public partial class taobao_addCategory : AdminPage
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        protected int parentid = SASRequest.GetInt("pid", 0);
        protected string ltitle = "添加顶级类别";
        protected TaoBaoPluginBase tbp = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (parentid > 0)
            {
                CategoryInfo parentinfo = tbp.GetCategoryInfo(parentid);
                if (parentinfo != null) ltitle = "添加 " + parentinfo.Name + " 相关子类别";
            }
        }

        private void AddCategoryInfo_Click(object sender, EventArgs e)
        {
            CategoryInfo cinfo = new CategoryInfo();
            cinfo.Name = Utils.RemoveHtml(cname.Text.Trim());
            cinfo.Displayorder = TypeConverter.ObjectToInt(displayorder.Text, 0);
            cinfo.Cg_status = TypeConverter.ObjectToInt(available.SelectedValue, 0);
            cinfo.Parentid = parentid;
            cinfo.Cg_relateclass = SASRequest.GetString("TargetFID");
            cinfo.Parentlist = "0";
            cinfo.Goodcount = 0;
            cinfo.Sort = 0;
            cinfo.Haschild = 0;
            if (parentid > 0)
            {
                CategoryInfo parentinfo = tbp.GetCategoryInfo(parentid);

                if (parentinfo == null)
                {
                    base.RegisterStartupScript("", "<script>alert('上级类别异常，请与管理员联系!');window.location.href='taobao_categorygrid.aspx';</script>");
                    return;
                }
                cinfo.Sort = parentinfo.Sort + 1;

                if (parentinfo.Parentlist.Trim() == "0")
                {
                    cinfo.Parentlist = parentinfo.Cid.ToString();
                }
                else
                {
                    cinfo.Parentlist = parentinfo.Parentlist + "," + parentinfo.Cid;
                }

                if (tbp.CreateCategoryInfo(cinfo) > 0)
                {
                    parentinfo.Haschild = 1;
                    tbp.UpdateCategoryInfo(parentinfo);
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CategoryList");
                    base.RegisterStartupScript("PAGE", "window.location.href='taobao_categorygrid.aspx';");                    
                }
                else
                {
                    base.RegisterStartupScript("pagetemplate", "父类更新失败！");
                    return;
                }
            }
            else
            {
                if (tbp.CreateCategoryInfo(cinfo) > 0)
                {
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CategoryList");
                    base.RegisterStartupScript("PAGE", "window.location.href='taobao_categorygrid.aspx';");                    
                }
                else
                {
                    base.RegisterStartupScript("pagetemplate", "类别添加失败！");
                    return;
                }
            }
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
