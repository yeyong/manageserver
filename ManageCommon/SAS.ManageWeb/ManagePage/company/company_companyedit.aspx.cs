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
                SourDataBind();
                if (SASRequest.GetInt("enid", 0) == 0)
                {
                    Response.Redirect("company_companygrid.aspx");
                    return;
                }

                LoadCompanyInfo(SASRequest.GetInt("enid", 0));
            }
        }

        private void SourDataBind()
        {
            EnTypeEnum ete = new EnTypeEnum();
            enType.Items.Clear();
            foreach (string cname in Enum.GetNames(ete.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ete.GetType(), cname));
                string s_text = EnumCatch.GetCompanyType(s_value);
                enType.Items.Add(new ListItem(s_text, s_value.ToString()));
            }

            CommTypeEnum cte = new CommTypeEnum();
            enco.Items.Clear();
            foreach (string ecname in Enum.GetNames(cte.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(cte.GetType(), ecname));
                string s_text = EnumCatch.GetEnCommType(s_value);
                enco.Items.Add(new ListItem(s_text, s_value.ToString()));
            }
        }

        /// <summary>
        /// 设置绑定企业原有数据
        /// </summary>
        /// <param name="enid"></param>
        private void LoadCompanyInfo(int enid)
        {
            _companyInfos = AdminCompanies.GetCompanyInfo(enid);
            qyname.Text = _companyInfos.En_name;
            status.SelectedValue = _companyInfos.En_visble.ToString();
            encorp.Text = _companyInfos.En_corp;
            encontact.Text = _companyInfos.En_contact;
            enphone.Text = _companyInfos.En_phone;
            enmobile.Text = _companyInfos.En_mobile;
            enfax.Text = _companyInfos.En_fax;
            enemail.Text=_companyInfos.En_mail;
            enweb.Text = _companyInfos.En_web;
            defaultarea = areas.GetCascadeString(_companyInfos.En_areas);
            enpost.Text= _companyInfos.En_post;
            enaddress.Text = _companyInfos.En_address;
            endesc.Text = _companyInfos.En_desc;
            enbuilddate.Text = _companyInfos.En_builddate;
            enType.SelectedValue = _companyInfos.En_type.ToString();
            enco.SelectedValue = _companyInfos.En_enco.ToString();
            regcapital.Text = Utils.StrToFloat(_companyInfos.Reg_capital, 0).ToString();
            regcode.Text = _companyInfos.Reg_code;
            regorgan.Text = _companyInfos.Reg_organ;
            regyear.Text = _companyInfos.Reg_year;
            regdate.Text = _companyInfos.Reg_date;
            regaddress.Text = _companyInfos.Reg_address;
            enmain.Text = _companyInfos.En_main;
            enstatus.SelectedValue = _companyInfos.En_status.ToString();
            enreason.Text = _companyInfos.En_reason;
            enlevels.SelectedValue = _companyInfos.En_level.ToString();
            encredit.Text = _companyInfos.En_credits.ToString();
            defaultcata = _companyInfos.En_cataloglist;
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

                if (SASRequest.GetString("hyidlist") == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请准确选择公司主营行业!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                int renid = AdminCompanies.ExistCompanyName(qyname.Text.Trim());
                if (renid != 0 && renid != SASRequest.GetInt("enid", 0))
                {
                    base.RegisterStartupScript("", "<script>alert('您填写的公司名称重复，请重新填写!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                Companys _companyInfo = AdminCompanies.GetCompanyInfo(SASRequest.GetInt("enid", 0));

                _companyInfo.En_name = qyname.Text.Trim();
                _companyInfo.En_visble = Convert.ToInt32(status.SelectedValue);
                _companyInfo.En_corp = encorp.Text.Trim();
                _companyInfo.En_contact = encontact.Text.Trim();
                _companyInfo.En_phone = enphone.Text;
                _companyInfo.En_mobile = enmobile.Text;
                _companyInfo.En_fax = enfax.Text;
                _companyInfo.En_mail = enemail.Text;
                _companyInfo.En_web = enweb.Text;
                _companyInfo.En_areas = SASRequest.GetInt("district", 0);
                _companyInfo.En_post = enpost.Text;
                _companyInfo.En_address = enaddress.Text.Trim();
                _companyInfo.En_desc = endesc.Text;
                _companyInfo.En_builddate = enbuilddate.Text;
                _companyInfo.En_type = Convert.ToInt32(enType.SelectedValue);
                _companyInfo.En_enco = Convert.ToInt32(enco.SelectedValue);
                _companyInfo.Reg_capital = regcapital.Text;
                _companyInfo.Reg_code = regcode.Text.Trim();
                _companyInfo.Reg_organ = regorgan.Text.Trim();
                _companyInfo.Reg_year = regyear.Text;
                _companyInfo.Reg_date = regdate.Text.Trim();
                _companyInfo.Reg_address = regaddress.Text.Trim();
                _companyInfo.En_main = enmain.Text.Trim();
                _companyInfo.En_status = Convert.ToInt32(enstatus.SelectedValue);
                _companyInfo.En_reason = enreason.Text.Trim();
                _companyInfo.En_level = Convert.ToInt32(enlevels.SelectedValue);
                _companyInfo.En_credits = TypeConverter.StrToInt(encredit.Text, 0);
                _companyInfo.En_cataloglist = Utils.ChkSQL(SASRequest.GetString("hyidlist"));

                _companyInfo.En_sell = 0;
                _companyInfo.En_logo = "";
                _companyInfo.En_music = "";

                if (!AdminCompanies.UpdateCompanyInfo(_companyInfo))
                {
                    base.RegisterStartupScript("", "<script>alert('修改操作失败，请与管理员联系!');window.location.href='company_companyedit.aspx?enid=" + SASRequest.GetInt("enid", 0) + "';</script>");
                    return;
                }

                foreach (string str in _companyInfo.En_cataloglist.Split(','))
                {
                    Caches.ReSetCompanyTableSub(TypeConverter.StrToInt(str, 0));
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
