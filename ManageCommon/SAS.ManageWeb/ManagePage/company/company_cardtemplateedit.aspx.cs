using System;
using System.IO;
using System.Text;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 名片模板文件编辑
    /// </summary>
    public partial class company_cardtemplateedit : AdminPage
    {
        public string filenamefullpath;
        public string path;
        public string filename;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = SASRequest.GetString("path");
            if (path == "")
            {
                Response.Redirect("company_cardtemplatetree.aspx");
                return;
            }

            filename = SASRequest.GetString("filename");
            filenamefullpath = "../../cardtemplate/" + path + "/" + filename;

            ViewState["path"] = path;
            ViewState["filename"] = filename;
            ViewState["templateid"] = SASRequest.GetString("templateid");
            ViewState["templatename"] = SASRequest.GetString("templatename");

            if (!Page.IsPostBack)
            {
                using (StreamReader objReader = new StreamReader(Server.MapPath(filenamefullpath), Encoding.UTF8))
                {
                    templatenew.Text = objReader.ReadToEnd();
                    objReader.Close();
                }
            }
        }

        private void SavaTemplateInfo_Click(object sender, EventArgs e)
        {
            #region 保存相关模板信息

            if (this.CheckCookie())
            {
                string path = ViewState["path"].ToString();
                string filename = ViewState["filename"].ToString();
                filenamefullpath = Server.MapPath("../../cardtemplate/" + path + "/" + filename);

                using (FileStream fs = new FileStream(filenamefullpath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = Encoding.UTF8.GetBytes(templatenew.Text);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

                base.RegisterStartupScript("PAGE", "window.location.href='company_cardtemplatetree.aspx?path=" + ViewState["path"].ToString().Split('\\')[0] + "&templateid=" + ViewState["templateid"].ToString() + "&templatename=" + ViewState["templatename"].ToString() + "';");
            }

            #endregion
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SavaTemplateInfo.Click += new EventHandler(this.SavaTemplateInfo_Click);
        }

        #endregion

    }
}
