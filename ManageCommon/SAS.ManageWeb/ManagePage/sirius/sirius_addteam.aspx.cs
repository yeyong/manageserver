using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Cache;
using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.Sirius;

namespace SAS.ManageWeb.ManagePage
{
    public partial class sirius_addteam : AdminPage
    {
        protected string root = Utils.GetRootUrl(BaseConfigs.GetBaseConfig().Sitepath);
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();

        public void InitInfo()
        {
            templateid.AddTableData(Templates.GetValidTemplateList(), "tp_name", "tp_id");
        }

        private void SubmitAddChild()
        {
            #region 保存新增团队信息
            if (this.CheckCookie())
            {
                if (name.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队名称不能为空');</script>");
                    return;
                }

                if (!Utils.IsSafeSqlString(name.Text.Trim()))
                {
                    base.RegisterStartupScript("", "<script>alert('您输入的团队名包含不安全的字符,因此无法提交!');window.location.href='sirius_addteam.aspx';</script>");
                    return;
                }

                if (PrivateMessages.SystemUserName == name.Text)
                {
                    base.RegisterStartupScript("", "<script>alert('您不能创建该团队名,因为它是系统保留的用户名,请您输入其它的团队名!');window.location.href='sirius_addteam.aspx';</script>");
                    return;
                }

                TeamInfo teaminfo = CreateTeamInfo();
                string results = "";
                spb.CreateTeamInfo(teaminfo, out results);

                if (results == "")
                {
                    base.RegisterStartupScript("PAGE", "self.location.href='sirius_manageteam.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("PAGE", "alert('用户:" + results + "不存在或不是指定用户组,因为无法设为团队成员');self.location.href='sirius_manageteam.aspx';");
                }
                AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "添加团队信息", "添加新团队名为：" + teaminfo.Name);
            }

            #endregion
        }

        private TeamInfo CreateTeamInfo()
        {
            TeamInfo teaminfo = new TeamInfo();

            teaminfo.Name = name.Text;
            teaminfo.Teamdomain = teamurl.Text.Trim();
            teaminfo.Templateid = Convert.ToInt32(templateid.SelectedValue);
            teaminfo.Imgs = teamImg.Text.Trim();
            teaminfo.Bio = teamBio.Text.Trim();
            teaminfo.Content1 = content1.Text;
            teaminfo.Content2 = content2.Text;
            teaminfo.Content3 = content3.Text;
            teaminfo.TeamMember = moderators.Text.Trim();
            teaminfo.Stutas = Convert.ToInt16(status.SelectedValue);
            teaminfo.Displayorder = 0;
            teaminfo.Seodescription = seodescription.Text.Trim();
            teaminfo.Seokeywords = seokeywords.Text.Trim();
            teaminfo.Creater = username;

            return teaminfo;
        }

        private void SubmitAdd_Click(object sender, EventArgs e)
        {
            SubmitAddChild();
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
            this.SubmitAdd.Click += new EventHandler(this.SubmitAdd_Click);
            InitInfo();
        }

        #endregion      
    }
}
