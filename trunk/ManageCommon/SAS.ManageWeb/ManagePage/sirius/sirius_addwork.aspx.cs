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
    public partial class sirius_addwork : AdminPage
    {
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void AddActInfo_Click(object sender, EventArgs e)
        {
            #region 保存新增团队成果信息
            if (this.CheckCookie())
            {
                if (title.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队成果名称不能为空');</script>");
                    return;
                }

                if (TypeConverter.StrToInt(teams.SelectedValue, 0) < 1)
                {
                    base.RegisterStartupScript("", "<script>alert('发起团队由主要团队组成，必须选择，因此无法提交!');window.location.href='sirius_addactivity.aspx';</script>");
                    return;
                }


                TeamWorkInfo ainfo = LoadWorkInfo();
                string results = "";
                spb.CreateWork(ainfo, out results);

                if (results == "")
                {
                    base.RegisterStartupScript("PAGE", "self.location.href='sirius_managework.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("PAGE", "alert('用户:" + results + "不存在或不是指定用户组,因为无法设为参与成员');self.location.href='sirius_managework.aspx';");
                }
                AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "添加团队成果信息", "添加新成果名为：" + ainfo.Name);
            }

            #endregion
        }

        private TeamWorkInfo LoadWorkInfo()
        {
            TeamWorkInfo tainfo = new TeamWorkInfo();
            tainfo.Name = title.Text.Trim();
            tainfo.Start = starttime.Text.Trim();
            tainfo.End = endtime.Text.Trim();
            tainfo.Worddesc = message.Text.Trim();
            tainfo.Img = listpic.Text.Trim();
            tainfo.Imgbak = listbak.Text.Trim();
            tainfo.Teamid = TypeConverter.StrToInt(teams.SelectedValue, 0);
            tainfo.Members = moderators.Text.Trim();
            tainfo.Url = url.Text.Trim();
            return tainfo;
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
            this.AddActInfo.Click += new EventHandler(this.AddActInfo_Click);

            teams.Items.Clear();
            teams.AddTableData(spb.GetAllTeam(), "name", "teamid");
        }

        #endregion
    }
}
