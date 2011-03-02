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
    public partial class sirius_editwork : AdminPage
    {
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();
        protected int wid = 0;

        private void UpActInfo_Click(object sender, EventArgs e)
        {
            #region 保存新增团队信息
            if (this.CheckCookie())
            {
                if (title.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队活动名称不能为空');</script>");
                    return;
                }

                if (TypeConverter.StrToInt(teams.SelectedValue, 0) < 1)
                {
                    base.RegisterStartupScript("", "<script>alert('发起团队由主要团队组成，必须选择，因此无法提交!');window.location.href='sirius_editwork.aspx?wid=" + wid + "';</script>");
                    return;
                }

                TeamWorkInfo ainfo = LoadActInfo();
                string message = "";
                spb.UpdateWorkInfo(ainfo, out message);
                if (message == "")
                {
                    base.RegisterStartupScript("PAGE", "self.location.href='sirius_managework.aspx?tid=" + ainfo.Teamid + "';");
                }
                else
                {
                    base.RegisterStartupScript("PAGE", "alert('用户:" + message + "不存在或不是指定用户组,因为无法设为团队成员');self.location.href='sirius_managework.aspx?tid=" + ainfo.Teamid + "';");
                }
                AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "修改团队成果信息", "更新成果信息：" + ainfo.Name);
            }

            #endregion
        }

        private TeamWorkInfo LoadActInfo()
        {
            TeamWorkInfo tainfo = new TeamWorkInfo();
            tainfo.Id = wid;
            tainfo.Name = title.Text.Trim();
            tainfo.Start = starttime.Text.Trim();
            tainfo.End = endtime.Text.Trim();
            tainfo.Worddesc = message.Text.Trim();
            tainfo.Img = listpic.Text.Trim();
            tainfo.Imgbak = listbak.Text.Trim();
            tainfo.Teamid = TypeConverter.StrToInt(teams.SelectedValue, 0);
            tainfo.Url = url.Text.Trim();
            tainfo.Members = moderators.Text.Trim();
            return tainfo;
        }

        private void BuildWorkInfo(int aid)
        {
            TeamWorkInfo tinfo = new TeamWorkInfo();
            tinfo = spb.GetWorkInfo(aid);
            title.Text = tinfo.Name;
            starttime.Text = tinfo.Start;
            endtime.Text = tinfo.End;
            message.Text = tinfo.Worddesc;
            listpic.Text = tinfo.Img;
            listbak.Text = tinfo.Imgbak;
            url.Text = tinfo.Url;
            moderators.Text = tinfo.Members;
            teams.SelectedValue = tinfo.Teamid.ToString();
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
            this.UpActInfo.Click += new EventHandler(this.UpActInfo_Click);
            if (SASRequest.GetInt("wid", 0) < 1)
            {
                base.RegisterStartupScript("PAGE", "alert('页面参数错误，请与管理员联系！');self.location.href='sirius_searchactivity.aspx';");
                return;
            }
            wid = SASRequest.GetInt("wid", 0);
            BuildWorkInfo(SASRequest.GetInt("wid", 0));
            teams.Items.Clear();
            teams.AddTableData(spb.GetAllTeam(), "name", "teamid");
        }
        #endregion
    }
}
