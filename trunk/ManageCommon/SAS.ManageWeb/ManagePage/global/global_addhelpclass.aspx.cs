using System;
using System.Web.UI;

using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_addhelpclass : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Utils.StrIsNullOrEmpty(username))
                    poster.Text = this.username;
            }
        }

        protected void add_Click(object sender, EventArgs e)
        {
            #region 增加帮助类别
            if (this.CheckCookie())
            {
                Helps.AddHelp(title.Text, "", 0);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "添加帮助分类", "添加帮助分类,标题为:" + title.Text);
                base.RegisterStartupScript("", "<script>window.location.href='global_helplist.aspx';</script>");
            }
            #endregion
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        #endregion
    }
}
