using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Entity;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_addcompany : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCatalogData();
            }
        }

        private void BindCatalogData()
        {

        }

        private void AddCompanyInfo_Click(object sender, EventArgs e)
        {
            #region 添加企业信息
            if (this.CheckCookie())
            {
                if (qyname.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('企业名称为空,因此无法提交!');window.location.href='global_addcompany.aspx';</script>");
                    return;
                }

                if (SASRequest.GetInt("district", 0) == 0)
                {
                    base.RegisterStartupScript("", "<script>alert('请准确选择公司所在地区!');window.location.href='global_addcompany.aspx';</script>");
                    return;
                }

                if (SASRequest.GetString("hyidlist") == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请准确选择公司主营行业!');window.location.href='global_addcompany.aspx';</script>");
                    return;
                }

                Companys _companyInfo = CreateCompanyInfo();

                if (AdminCompanies.ExistCompanyName(_companyInfo.en_name) != 0)
                {
                    base.RegisterStartupScript("", "<script>alert('您填写的公司名称重复，请重新填写!');window.location.href='global_addcompany.aspx';</script>");
                    return;
                }

                int enid = AdminCompanies.CreateCompanyInfo(_companyInfo);

                if (enid == 0)
                {
                    base.RegisterStartupScript("", "<script>alert('添加操作失败，请与管理员联系!');window.location.href='global_addcompany.aspx';</script>");
                    return;
                }

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台添加企业信息", "企业名:" + qyname.Text.Trim());

                base.RegisterStartupScript("PAGE", "window.location.href='company_companygrid.aspx';");
            }
            #endregion
        }

        /// <summary>
        /// 建立企业信息实体
        /// </summary>
        /// <returns></returns>
        private Companys CreateCompanyInfo()
        {
            Companys comps = new Companys();
            comps.en_name = qyname.Text.Trim();
            comps.en_visble = Convert.ToInt32(status.SelectedValue);
            comps.en_corp = encorp.Text.Trim();
            comps.en_contact = encontact.Text.Trim();
            comps.en_phone = enphone.Text;
            comps.en_mobile = enmobile.Text;
            comps.en_fax = enfax.Text;
            comps.en_mail = enemail.Text;
            comps.en_web = enweb.Text;
            comps.en_areas = SASRequest.GetInt("district", 0);
            comps.en_post = enpost.Text;
            comps.en_address = enaddress.Text.Trim();
            comps.en_desc = endesc.Text;
            comps.en_builddate = enbuilddate.Text;
            comps.en_type = Convert.ToInt32(enType.SelectedValue);
            comps.en_enco = Convert.ToInt32(enco.SelectedValue);
            comps.reg_capital = regcapital.Text;
            comps.reg_code = regcode.Text.Trim();
            comps.reg_organ = regorgan.Text.Trim();
            comps.reg_year = regyear.Text;
            comps.reg_date = regdate.Text.Trim();
            comps.reg_address = regaddress.Text.Trim();
            comps.en_main = enmain.Text.Trim();
            comps.en_status = Convert.ToInt32(enstatus.SelectedValue);
            comps.en_reason = enreason.Text.Trim();
            comps.en_level = Convert.ToInt32(enlevels.SelectedValue);
            comps.en_credits = TypeConverter.StrToInt(encredit.Text, 0);
            comps.en_cataloglist = Utils.ChkSQL(SASRequest.GetString("hyidlist"));

            comps.en_sell = 0;
            comps.en_logo = "";
            comps.en_music = "";
            return comps;
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            this.AddCompanyInfo.Click += new EventHandler(AddCompanyInfo_Click);
        }

        #endregion        
    }
}
