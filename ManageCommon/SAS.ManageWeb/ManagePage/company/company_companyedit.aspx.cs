using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Entity;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    public partial class company_companyedit : AdminPage
    {
        protected Companys _companyInfos = new Companys();

        protected string defaultarea = "";
        protected string defaultcata = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SASRequest.GetInt("enid", 0) == 0)
                {
                    Response.Redirect("company_companygrid.aspx");
                    return;
                }

                LoadCompanyInfo(SASRequest.GetInt("enid", 0));
            }
        }

        /// <summary>
        /// 设置绑定企业原有数据
        /// </summary>
        /// <param name="enid"></param>
        private void LoadCompanyInfo(int enid)
        {
            _companyInfos = AdminCompanies.GetCompanyInfo(enid);
            qyname.Text = _companyInfos.en_name;
            status.SelectedValue = _companyInfos.en_visble.ToString();
            encorp.Text = _companyInfos.en_corp;
            encontact.Text = _companyInfos.en_contact;
            enphone.Text = _companyInfos.en_phone;
            enmobile.Text = _companyInfos.en_mobile;
            enfax.Text = _companyInfos.en_fax;
            enemail.Text=_companyInfos.en_mail;
            enweb.Text = _companyInfos.en_web;
            defaultarea = areas.GetCascadeString(_companyInfos.en_areas);
            enpost.Text= _companyInfos.en_post;
            enaddress.Text = _companyInfos.en_address;
            endesc.Text = _companyInfos.en_desc;
            enbuilddate.Text = _companyInfos.en_builddate;
            enType.SelectedValue = _companyInfos.en_type.ToString();
            enco.SelectedValue = _companyInfos.en_enco.ToString();
            regcapital.Text = Utils.StrToFloat(_companyInfos.reg_capital, 0).ToString();
            regcode.Text = _companyInfos.reg_code;
            regorgan.Text = _companyInfos.reg_organ;
            regyear.Text = _companyInfos.reg_year;
            regdate.Text = _companyInfos.reg_date;
            regaddress.Text = _companyInfos.reg_address;
            enmain.Text = _companyInfos.en_main;
            enstatus.SelectedValue = _companyInfos.en_status.ToString();
            enreason.Text = _companyInfos.en_reason;
            enlevels.SelectedValue = _companyInfos.en_level.ToString();
            encredit.Text = _companyInfos.en_credits.ToString();
            defaultcata = _companyInfos.en_cataloglist;
        }

        private void UpdateCompanyInfo_Click(object sender, EventArgs e)
        {
            #region 修改企业信息
            if (this.CheckCookie())
            {
                if (qyname.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('企业名称为空,因此无法提交!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                if (SASRequest.GetInt("district", 0) == 0)
                {
                    base.RegisterStartupScript("", "<script>alert('请准确选择公司所在地区!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }
                int renid = AdminCompanies.ExistCompanyName(qyname.Text.Trim());
                if (renid != 0 && renid != SASRequest.GetInt("enid", 0))
                {
                    base.RegisterStartupScript("", "<script>alert('您填写的公司名称重复，请重新填写!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                Companys _companyInfo = AdminCompanies.GetCompanyInfo(SASRequest.GetInt("enid", 0));

                _companyInfo.en_name = qyname.Text.Trim();
                _companyInfo.en_visble = Convert.ToInt32(status.SelectedValue);
                _companyInfo.en_corp = encorp.Text.Trim();
                _companyInfo.en_contact = encontact.Text.Trim();
                _companyInfo.en_phone = enphone.Text;
                _companyInfo.en_mobile = enmobile.Text;
                _companyInfo.en_fax = enfax.Text;
                _companyInfo.en_mail = enemail.Text;
                _companyInfo.en_web = enweb.Text;
                _companyInfo.en_areas = SASRequest.GetInt("district", 0);
                _companyInfo.en_post = enpost.Text;
                _companyInfo.en_address = enaddress.Text.Trim();
                _companyInfo.en_desc = endesc.Text;
                _companyInfo.en_builddate = enbuilddate.Text;
                _companyInfo.en_type = Convert.ToInt32(enType.SelectedValue);
                _companyInfo.en_enco = Convert.ToInt32(enco.SelectedValue);
                _companyInfo.reg_capital = regcapital.Text;
                _companyInfo.reg_code = regcode.Text.Trim();
                _companyInfo.reg_organ = regorgan.Text.Trim();
                _companyInfo.reg_year = regyear.Text;
                _companyInfo.reg_date = regdate.Text.Trim();
                _companyInfo.reg_address = regaddress.Text.Trim();
                _companyInfo.en_main = enmain.Text.Trim();
                _companyInfo.en_status = Convert.ToInt32(enstatus.SelectedValue);
                _companyInfo.en_reason = enreason.Text.Trim();
                _companyInfo.en_level = Convert.ToInt32(enlevels.SelectedValue);
                _companyInfo.en_credits = TypeConverter.StrToInt(encredit.Text, 0);

                _companyInfo.en_sell = 0;
                _companyInfo.en_logo = "";
                _companyInfo.en_music = "";

                if (!AdminCompanies.UpdateCompanyInfo(_companyInfo))
                {
                    base.RegisterStartupScript("", "<script>alert('修改操作失败，请与管理员联系!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台修改企业信息", "企业名:" + qyname.Text.Trim());

                base.RegisterStartupScript("PAGE", "window.location.href='company_companygrid.aspx';");
            }
            #endregion
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
            this.UpdateCompanyInfo.Click += new EventHandler(UpdateCompanyInfo_Click);            
        }

        #endregion        
    }
}
