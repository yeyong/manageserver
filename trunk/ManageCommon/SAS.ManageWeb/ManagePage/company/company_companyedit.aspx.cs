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
        }

        private void UpdateCompanyInfo_Click(object sender, EventArgs e)
        {
            #region 添加企业信息
            if (this.CheckCookie())
            {
                if (qyname.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('企业名称为空,因此无法提交!');window.location.href='company_companyedit.aspx';</script>");
                    return;
                }

                if (SASRequest.GetInt("district", 0) == 0)
                {
                    base.RegisterStartupScript("", "<script>alert('请准确选择公司所在地区!');window.location.href='company_companyedit.aspx';</script>");
                    return;
                }
                Companys _companyInfo = AdminCompanies.GetCompanyInfo(SASRequest.GetInt("enid", 0));

                if (AdminCompanies.ExistCompanyName(qyname.Text.Trim()) != 0)
                {
                    base.RegisterStartupScript("", "<script>alert('您填写的公司名称重复，请重新填写!');window.location.href='company_companyedit.aspx';</script>");
                    return;
                }

                //int enid = AdminCompanies.CreateCompanyInfo(_companyInfo);

                //if (enid == 0)
                //{
                //    base.RegisterStartupScript("", "<script>alert('添加操作失败，请与管理员联系!');window.location.href='company_companyedit.aspx';</script>");
                //    return;
                //}

                //AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台添加企业信息", "企业名:" + qyname.Text.Trim());

                //base.RegisterStartupScript("PAGE", "window.location.href='company_companygrid.aspx';");
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
