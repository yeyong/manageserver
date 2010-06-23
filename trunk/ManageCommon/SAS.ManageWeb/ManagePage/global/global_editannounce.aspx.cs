using System;
using System.Data;
using System.Web.UI;

using SAS.Common;
using SAS.Control;
using SAS.Config;
using SAS.Entity;
using SAS.Logic;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_editannounce : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SASRequest.GetString("id") == "")
                {
                    Response.Redirect("global_announcegrid.aspx");
                }
                else
                {
                    LoadAnnounceInf(SASRequest.GetInt("id", -1));
                    UpdateAnnounceInfo.ValidateForm = true;
                    title.AddAttributes("maxlength", "200");
                    title.AddAttributes("rows", "2");
                }
            }
        }

        public void LoadAnnounceInf(int id)
        {
            #region 装载公告信息
            AnnouncementInfo announcement = Logic.Announcements.GetAnnouncement(id);
            if (announcement == null)
                return;

            displayorder.Text = announcement.Displayorder.ToString();
            title.Text = announcement.Title;
            poster.Text = announcement.Poster;
            starttime.Text = Utils.GetStandardDateTime(announcement.Starttime.ToString());
            endtime.Text = Utils.GetStandardDateTime(announcement.Endtime.ToString());
            message.Text = announcement.Message;

            #endregion
        }

        private void UpdateAnnounceInfo_Click(object sender, EventArgs e)
        {
            #region 更新公告相关信息

            if (this.CheckCookie())
            {
                AnnouncementInfo announcementInfo = new AnnouncementInfo();
                announcementInfo.Id = SASRequest.GetInt("id", 0);
                announcementInfo.Poster = poster.Text.Trim();
                announcementInfo.Title = title.Text.Trim();
                announcementInfo.Displayorder = TypeConverter.StrToInt(displayorder.Text);
                announcementInfo.Starttime = Convert.ToDateTime(starttime.Text);
                announcementInfo.Endtime = Convert.ToDateTime(endtime.Text);
                announcementInfo.Message = message.Text.Trim();
                announcementInfo.Relateactive = SASRequest.GetString("TargetFID");
                Announcements.UpdateAnnouncement(announcementInfo);
                //移除公告缓存
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/AnnouncementList");
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SimplifiedAnnouncementList");

                //记录日志
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "更新公告", "更新公告,标题为:" + title.Text);
                base.RegisterStartupScript("PAGE", "window.location.href='global_announcegrid.aspx';");
            }
            #endregion
        }

        private void DeleteAnnounce_Click(object sender, EventArgs e)
        {
            #region 删除公告

            if (this.CheckCookie())
            {
                Announcements.DeleteAnnouncements(SASRequest.GetString("id"));
                //记录日志
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "删除公告", "删除公告,标题为:" + title.Text);
                base.RegisterStartupScript("PAGE", "window.location.href='global_announcegrid.aspx';");
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
            this.UpdateAnnounceInfo.Click += new EventHandler(this.UpdateAnnounceInfo_Click);
            this.DeleteAnnounce.Click += new EventHandler(this.DeleteAnnounce_Click);
        }

        #endregion
    }
}
