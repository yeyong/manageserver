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
                SourDataBind();
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

            enconfig.AddTableData(CardConfigs.GetCardConfigList(), "ccname", "id");
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

                if (AdminCompanies.ExistCompanyName(_companyInfo.En_name) != 0)
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
            comps.En_name = qyname.Text.Trim();
            comps.En_visble = Convert.ToInt32(status.SelectedValue);
            comps.En_corp = encorp.Text.Trim();
            comps.En_contact = encontact.Text.Trim();
            comps.En_phone = enphone.Text;
            comps.En_mobile = enmobile.Text;
            comps.En_fax = enfax.Text;
            comps.En_mail = enemail.Text;
            comps.En_web = enweb.Text;
            comps.En_areas = SASRequest.GetInt("district", 0);
            comps.En_post = enpost.Text;
            comps.En_address = enaddress.Text.Trim();
            comps.En_desc = endesc.Text;
            comps.En_builddate = enbuilddate.Text;
            comps.En_type = Convert.ToInt32(enType.SelectedValue);
            comps.En_enco = Convert.ToInt32(enco.SelectedValue);
            comps.Reg_capital = regcapital.Text;
            comps.Reg_code = regcode.Text.Trim();
            comps.Reg_organ = regorgan.Text.Trim();
            comps.Reg_year = regyear.Text;
            comps.Reg_date = regdate.Text.Trim();
            comps.Reg_address = regaddress.Text.Trim();
            comps.En_main = enmain.Text.Trim();
            comps.En_status = Convert.ToInt32(enstatus.SelectedValue);
            comps.En_reason = enreason.Text.Trim();
            comps.En_level = Convert.ToInt32(enlevels.SelectedValue);
            comps.En_credits = TypeConverter.StrToInt(encredit.Text, 0);
            comps.En_cataloglist = Utils.ChkSQL(SASRequest.GetString("hyidlist"));

            comps.En_sell = 0;
            comps.En_logo = "";
            comps.En_music = "";
            comps.Configid = Convert.ToInt32(enconfig.SelectedValue);
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
