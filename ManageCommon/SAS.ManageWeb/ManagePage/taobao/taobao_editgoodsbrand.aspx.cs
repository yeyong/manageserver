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
    public partial class taobao_editgoodsbrand : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();
        protected GoodsBrandInfo ginfo = new GoodsBrandInfo();
        protected int bid = SASRequest.GetInt("id", 0);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ginfo == null)
                {
                    base.RegisterStartupScript("", "<script>alert('参数传递错误！');window.location.href='taobao_goodsbrandgrid.aspx';</script>");
                    return;
                }
                InitGoodsBrandInfo();
            }
        }

        private void InitGoodsBrandInfo()
        {
            brandname.Text = ginfo.bname;
            brandspell.Text = ginfo.spell;
            brandwebsite.Text = ginfo.website;
            brandcompany.Text = ginfo.bcompany;
            brandorder.Text = ginfo.order.ToString();
            brandlogo.Text = ginfo.logo;
            brandimg.Text = ginfo.img;
            seokeyword.Text = ginfo.keyword;
            brandshortdesc.Text = ginfo.shortdesc;
            branddesc.Text = ginfo.detaildesc;
            brandclass.SelectedValue = ginfo.relateclass;
            brandstatus.SelectedValue = ginfo.status.ToString();
        }

        private void UpdateBrandInfo_Click(object sender, EventArgs e)
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

                tpb.UpdateGoodsBrand(LoadGoodsBrandInfo());
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/GoodsBrand/Class_" + brandclass.SelectedValue);
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/GoodsBrand/Class_" + ginfo.relateclass);
                //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/GoodsBrand/Class_" + ginfo.relateclass, true);
                //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/GoodsBrand/Class_" + brandclass.SelectedValue, true);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "修改品牌", "编辑品牌,品牌名称:" + brandname.Text);
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
            brandinfo.id = bid;
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
            UpdateBrandInfo.Click += new EventHandler(UpdateBrandInfo_Click);
            brandclass.BuildTree(tpb.GetAllCategoryList(), "name", "cid");
            ginfo = tpb.GetGoodsBrandInfo(bid);
        }

        #endregion
    }
}
