using System;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Entity;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_addgoodsbrand : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void AddBrandInfo_Click(object sender, EventArgs e)
        {
            #region 添加活动
            if (this.CheckCookie())
            {
                if (brandname.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请填写品牌名称，品牌名称不可为空！');</script>");
                    return;
                }
                if (brandspell.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请填写品牌别名，品牌别名不可为空！');</script>");
                    return;
                }
                if (brandlogo.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请填写品牌Logo，品牌Logo不可为空！');</script>");
                    return;
                }

                int rows = tpb.CreateGoodsBrand(LoadGoodsBrandInfo());
                SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/GoodsBrand/Class_" + brandclass.SelectedValue, true);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "增加品牌", "创建新品牌,品牌名称:" + brandname.Text);
                base.RegisterStartupScript("PAGE", "window.location.href='taobao_goodsbrandgrid.aspx';");
            }
            #endregion
        }

        /// <summary>
        /// 加载活动实体信息
        /// </summary>
        /// <returns></returns>
        private GoodsBrandInfo LoadGoodsBrandInfo()
        {
            GoodsBrandInfo brandinfo = new GoodsBrandInfo();
            brandinfo.bname = brandname.Text.Trim();
            brandinfo.spell = brandspell.Text.Trim();
            brandinfo.website = brandwebsite.Text.Trim();
            brandinfo.bcompany = Utils.RemoveHtml(brandcompany.Text.Trim());
            brandinfo.order = TypeConverter.ObjectToInt(brandorder.Text.Trim(), 0);
            brandinfo.logo = brandlogo.Text.Trim();
            brandinfo.img = brandimg.Text.Trim();
            brandinfo.keyword = Utils.RemoveHtml(seokeyword.Text.Trim());
            brandinfo.shortdesc = Utils.RemoveHtml(brandshortdesc.Text.Trim());
            brandinfo.detaildesc = Utils.RemoveUnsafeHtml(branddesc.Text.Trim());
            brandinfo.relateclass = brandclass.SelectedValue;
            brandinfo.status = TypeConverter.StrToInt(brandstatus.SelectedValue, 0);
            return brandinfo;
        }

        #region 把VIEWSTATE写入容器

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SASLogicSavePageState(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            return base.DiscuzForumLoadPageState();
        }

        #endregion

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            AddBrandInfo.Click += new EventHandler(AddBrandInfo_Click);

            ActivityType ate = new ActivityType();
            brandclass.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
        }

        #endregion
    }
}
