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
    public partial class sirius_editactivity : AdminPage
    {
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();
        /// <summary>
        /// 初始化图片组
        /// </summary>
        protected string picsliststr = "";
        protected int actid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
                    base.RegisterStartupScript("", "<script>alert('发起团队由主要团队组成，必须选择，因此无法提交!');window.location.href='sirius_addactivity.aspx';</script>");
                    return;
                }

                TeamActInfo ainfo = LoadActInfo();
                spb.UpdateTeamAct(ainfo);
                base.RegisterStartupScript("PAGE", "self.location.href='sirius_manageactivity.aspx?tid=" + teams.SelectedValue + "';");
                AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "添加团队活动信息", "添加活动名为：" + ainfo.Name);
            }

            #endregion
        }

        private TeamActInfo LoadActInfo()
        {
            TeamActInfo tainfo = new TeamActInfo();
            tainfo.Id = actid;
            tainfo.Name = title.Text.Trim();
            tainfo.Start = starttime.Text.Trim();
            tainfo.End = endtime.Text.Trim();
            tainfo.Shortdesc = shortdesc.Text.Trim();
            tainfo.Img = listpic.Text.Trim();
            tainfo.Imgbak = listbak.Text.Trim();
            tainfo.Teamid = TypeConverter.StrToInt(teams.SelectedValue, 0);
            tainfo.Atype = 0;
            tainfo.Piccollect = SASRequest.GetString("selitems").Trim().Trim(',');
            return tainfo;
        }

        private void BuildActInfo(int aid)
        {
            TeamActInfo tinfo = new TeamActInfo();
            tinfo = spb.GetTeamActInfo(aid);
            picsliststr = tinfo.Piccollect;
            title.Text = tinfo.Name;
            starttime.Text = tinfo.Start;
            endtime.Text = tinfo.End;
            shortdesc.Text = tinfo.Shortdesc;
            listpic.Text = tinfo.Img;
            listbak.Text = tinfo.Imgbak;
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
            if (SASRequest.GetInt("actid", 0) < 1)
            {
                base.RegisterStartupScript("PAGE", "alert('页面参数错误，请与管理员联系！');self.location.href='sirius_searchactivity.aspx';");
                return;
            }
            actid = SASRequest.GetInt("actid", 0);
            BuildActInfo(SASRequest.GetInt("actid", 0));
            teams.Items.Clear();
            teams.AddTableData(spb.GetAllTeam(), "name", "teamid");
        }
        #endregion
    }
}
